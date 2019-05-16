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
    public class ProductServiceRetrievesOneProduct : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;
        private Product _result;

        protected override void When()
        {
            _productService = SUT.BuildProductService();

            SetupRepositoryToReturnOneProduct();

            var task = Task.Run(async () =>
            {
                _result = await _productService.GetProduct("id1").ConfigureAwait(false);
            });
            
            task.Wait();
        }

        private void SetupRepositoryToReturnOneProduct()
        {
            var product = Builder<ProductEntity>.CreateNew()
                .With(p => p.Id = "id1")
                .And(p => p.Description = "fancy product")
                .Build();
            SUT.ProductRepository.Setup(repo => repo.GetProduct(It.IsAny<string>())).ReturnsAsync(product);
        }

        [Test]
        public void ThenTheProductIsObtainedFromTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.GetProduct("id1"), Times.Once);
        }

        [Test]
        public void ThenTheProductIsReturned()
        {
            _result.Should().NotBeNull();
            _result.Id.Should().Be("id1");
            _result.Description.Should().Be("fancy product");
        }
    }
}
