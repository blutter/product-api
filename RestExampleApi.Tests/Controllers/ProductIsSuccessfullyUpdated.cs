using System;
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
    public class ProductIsSuccessfullyUpdated : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private Product _product;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            SetupServiceToRecordProduct();

            var request = Builder<ProductPutRequest>.CreateNew()
                .With(req => req.Description = "latest gizmo")
                .And(req => req.Brand = "new co")
                .And(req => req.Model = null)
                .Build();

            var task = Task.Run(async () =>
            {
                _result = await _controller.Put("id1", request).ConfigureAwait(false);
            });

            task.Wait();
        }

        private void SetupServiceToRecordProduct()
        {
            SUT.ProductService.Setup(service => service.UpdateProduct(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Callback<Product>(product => _product = product);
        }

        [Test]
        public void ThenTheProductIsUpdatedUsingTheService()
        {
            SUT.ProductService.Verify(repo => repo.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void ThenAnOkResultIsReturned()
        {
            _result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void ThenTheProductIdAndInformationIsPassedThrough()
        {
            _product.Id.Should().Be("id1");
            _product.Description.Should().Be("latest gizmo");
            _product.Brand.Should().Be("new co");
        }

        [Test]
        public void ThenTheNullModelFieldIsPassedThrough()
        {
            _product.Model.Should().BeNull();
        }
    }
}
