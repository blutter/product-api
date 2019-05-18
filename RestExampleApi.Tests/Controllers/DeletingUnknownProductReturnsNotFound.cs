using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExampleApi.Controllers;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class DeletingUnknownProductReturnsNotFound : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToThrowNotFoundException();

            var task = Task.Run(async () =>
            {
                _result = await _controller.Delete("id").ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToThrowNotFoundException()
        {
            SUT.ProductService.Setup(service => service.DeleteProduct(It.IsAny<string>())).ThrowsAsync(new KeyNotFoundException());
        }

        [Test]
        public void ThenTheProductIsDeletedUsingTheService()
        {
            SUT.ProductService.Verify(service => service.DeleteProduct("id"), Times.Once);
        }

        [Test]
        public void ThenANotFoundIsReturned()
        {
            _result.Should().BeOfType<NotFoundResult>();
        }
    }
}
