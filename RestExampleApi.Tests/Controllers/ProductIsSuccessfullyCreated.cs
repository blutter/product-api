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
    public class ProductIsSuccessfullyCreated : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _response;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToReturnProductId();

            var request = Builder<ProductPostRequest>.CreateNew()
                .With(req => req.Description = "latest gizmo")
                .Build();

            var task = Task.Run(async () =>
            {
                _response = await _controller.Post(request).ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToReturnProductId()
        {
            SUT.ProductService.Setup(service => service.CreateProduct(It.IsAny<Product>())).ReturnsAsync("1234");
        }

        [Test]
        public void ThenTheProductIsCreatedUsingTheService()
        {
            SUT.ProductService.Verify(repo => repo.CreateProduct(It.Is<Product>(p => p.Description == "latest gizmo")), Times.Once);
        }

        [Test]
        public void ThenOnOkIsReturned()
        {
            _response.Should().BeOfType<OkNegotiatedContentResult<ProductPostResponse>>();
        }

        [Test]
        public void ThenTheProductIdIsReturned()
        {
            var response = _response as OkNegotiatedContentResult<ProductPostResponse>;
            response.Should().NotBeNull();
            response.Content.Id.Should().Be("1234");
        }
    }
}
