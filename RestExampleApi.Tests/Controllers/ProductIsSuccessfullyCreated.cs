using System;
using System.Threading.Tasks;
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
        private ProductPostResponse _response;

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
        public void ThenTheProductIdIsReturned()
        {
            _response.Id.Should().Be("1234");
        }
    }
}
