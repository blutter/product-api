using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using SpecsFor.StructureMap;

namespace RestExample.Tests.ServiceTests
{
    public class CreateProductWithProductService : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;
        private Product _product;
        private string _result;

        protected override void When()
        {
            _productService = SUT.BuildProductService();
            _product = Builder<Product>.CreateNew()
                .With(p => p.Description = "fancy product")
                .Build();

            SetupRepositoryToReturnId();

            var task = Task.Run(async () =>
            {
                _result = await _productService.CreateProduct(_product).ConfigureAwait(false);
            });
            
            task.Wait();
        }

        private void SetupRepositoryToReturnId()
        {
            SUT.ProductRepository.Setup(repo => repo.CreateProduct(It.IsAny<ProductEntity>())).ReturnsAsync("newId");
        }

        [Test]
        public void ThenTheProductsAreCreatedInTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.CreateProduct(It.Is<ProductEntity>(entity => entity.Description == "fancy product")), Times.Once);
        }

        [Test]
        public void ThenTheProductIdIsReturned()
        {
            _result.Should().Be("newId");
        }
    }
}
