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
    public class GetByIdReturnsProduct : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private ProductResponse _result;

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
            var product = Builder<Product>.CreateNew()
                .With(prod => prod.Id = "newId")
                .Build();
            SUT.ProductService.Setup(repo => repo.GetProduct(It.IsAny<string>())).ReturnsAsync(product);
        }

        [Test]
        public void ThenTheProductIsReadFromTheRepository()
        {
            SUT.ProductService.Verify(repo => repo.GetProduct("id"), Times.Once);
        }

        [Test]
        public void ThenTheProductIsReturned()
        {
            _result.Id.Should().Be("newId");
        }
    }
}
