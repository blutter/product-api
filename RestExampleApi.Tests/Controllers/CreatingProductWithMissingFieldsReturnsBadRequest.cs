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
    public class CreatingProductWithMissingFieldsReturnsBadRequest : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _response;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            var request = Builder<ProductPostRequest>.CreateNew()
                .Build();

            var task = Task.Run(async () =>
            {
                _controller.ModelState.AddModelError("Model error", "something missing");
                _response = await _controller.Post(request).ConfigureAwait(false);
            });

            task.Wait();
        }

        [Test]
        public void ThenTheProductIsNotCreatedUsingTheService()
        {
            SUT.ProductService.Verify(repo => repo.CreateProduct(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public void ThenBadResultAsAInvalidModelStateResultIsReturned()
        {
            _response.Should().BeOfType<InvalidModelStateResult>();
        }
    }
}
