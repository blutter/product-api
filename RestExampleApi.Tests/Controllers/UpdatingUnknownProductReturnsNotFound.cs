using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using RestExampleApi.Controllers;
using RestExampleApi.Models;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class UpdatingUnknownProductReturnsNotFound : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToThrowNotFoundException();

            var request = Builder<ProductPutRequest>.CreateNew().Build();
            var task = Task.Run(async () =>
            {
                _result = await _controller.Put("id", request).ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToThrowNotFoundException()
        {
            SUT.ProductService.Setup(service => service.UpdateProduct(It.IsAny<Product>())).ThrowsAsync(new KeyNotFoundException());
        }

        [Test]
        public void ThenANotFoundIsReturned()
        {
            _result.Should().BeOfType<NotFoundResult>();
        }
    }
}
