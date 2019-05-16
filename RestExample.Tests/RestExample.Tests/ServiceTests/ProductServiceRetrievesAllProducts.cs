using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using RestExample.Services;
using SpecsFor.StructureMap;

namespace RestExample.Tests.ServiceTests
{
    public class ProductServiceRetrievesAllProducts : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;
        private List<Product> _result;

        protected override void When()
        {
            _productService = SUT.BuildProductService();

            SetupRepositoryToReturnThreeProducts();

            var task = Task.Run(async () =>
            {
                _result = await _productService.GetAllProducts().ConfigureAwait(false);
            });
            
            task.Wait();
        }

        private void SetupRepositoryToReturnThreeProducts()
        {
            var productList = Builder<ProductEntity>.CreateListOfSize(3).Build().ToList();
            SUT.ProductRepository.Setup(repo => repo.GetAllProducts()).ReturnsAsync(productList);
        }

        [Test]
        public void ThenTheProductsAreObtainedFromTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.GetAllProducts(), Times.Once);
        }

        [Test]
        public void ThenTheProductsAreReturned()
        {
            _result.Should().HaveCount(3);
        }
    }
}
