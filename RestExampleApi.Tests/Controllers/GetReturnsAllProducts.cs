using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExampleApi.Controllers;
using RestExampleApi.Models;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class GetReturnsAllProducts : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToReturnProducts();

            var task = Task.Run(async () =>
            {
                _result = await _controller.Get().ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToReturnProducts()
        {
            var products = Builder<Product>.CreateListOfSize(5)
                .Build().ToList();
            SUT.ProductService.Setup(service => service.GetAllProducts()).ReturnsAsync(products);
        }

        [Test]
        public void ThenTheProductsAreReadFromTheService()
        {
            SUT.ProductService.Verify(service => service.GetAllProducts(), Times.Once);
        }

        [Test]
        public void ThenOnOkIsReturned()
        {
            _result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<ProductResponse>>>();
        }

        [Test]
        public void ThenTheProductsAreReturned()
        {
            var result = _result as OkNegotiatedContentResult<IEnumerable<ProductResponse>>;
            result.Should().NotBeNull();
            result.Content.Should().HaveCount(5);
        }
    }
}
