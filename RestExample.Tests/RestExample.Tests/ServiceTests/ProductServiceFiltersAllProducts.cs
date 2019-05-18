using System.Collections.Generic;
using System.Linq;
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
    public class ProductServiceFiltersAllProducts : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;
        private List<Product> _result;

        protected override void When()
        {
            _productService = SUT.BuildProductService();

            SetupRepositoryToReturnThreeProducts();

            var productFilter = Builder<ProductFilter>.CreateNew()
                .With(filter => filter.Description = "washing")
                .And(filter => filter.Brand = null)
                .And(filter => filter.Model = null)
                .Build();

            var task = Task.Run(async () =>
            {
                _result = await _productService.GetAllProducts(productFilter).ConfigureAwait(false);
            });
            
            task.Wait();
        }

        private void SetupRepositoryToReturnThreeProducts()
        {
            var productList = Builder<ProductEntity>.CreateListOfSize(3).Build().ToList();
            SUT.ProductRepository.Setup(repo => repo.GetAllProducts(It.IsAny<ProductRepositoryFilter>())).ReturnsAsync(productList);
        }

        [Test]
        public void ThenTheProductsAreObtainedFromTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.GetAllProducts(
                It.Is<ProductRepositoryFilter>(filter => filter.Description == "washing" && filter.Brand == null && filter.Model == null)), Times.Once);
        }

        [Test]
        public void ThenTheProductsAreReturned()
        {
            _result.Should().HaveCount(3);
        }
    }
}
