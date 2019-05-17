using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExampleApi.Controllers;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class UnknownProductGetByIdReturnsNotFound : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToReturnProduct();

            var task = Task.Run(async () =>
            {
                _result = await _controller.Get("id").ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToReturnProduct()
        {
            Product nullProduct = null;
            SUT.ProductService.Setup(repo => repo.GetProduct(It.IsAny<string>())).ReturnsAsync(nullProduct);
        }

        [Test]
        public void ThenANotFoundIsReturned()
        {
            _result.Should().BeOfType<NotFoundResult>();
        }
    }
}
