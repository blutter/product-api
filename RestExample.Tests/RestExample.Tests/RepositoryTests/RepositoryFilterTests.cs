using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExample.Tests.RepositoryTests
{
    public class RepositoryFilterTests
    {
        [Test]
        public async Task FilterByDescription_ReturnsProduct()
        {
            // arrange
            var repo = await CreateRepositoryWithProducts();

            // act
            var products = await repo.GetAllProducts(new ProductRepositoryFilter { Description = "Super"});

            // assert
            products.Should().HaveCount(1);
            products.First().Description.Should().Be("Super product");
        }

        [Test]
        public async Task FilterByBrand_ReturnsProduct()
        {
            // arrange
            var repo = await CreateRepositoryWithProducts();

            // act
            var products = await repo.GetAllProducts(new ProductRepositoryFilter { Brand = "Reliable" });

            // assert
            products.Should().HaveCount(1);
            products.First().Description.Should().Be("Super product");
        }

        [Test]
        public async Task FilterByModel_ReturnsProduct()
        {
            // arrange
            var repo = await CreateRepositoryWithProducts();

            // act
            var products = await repo.GetAllProducts(new ProductRepositoryFilter { Model = "Average" });

            // assert
            products.Should().HaveCount(2);
            var expectedProductDescriptions = new List<string> { "Average product", "Better product" };
            products.Select(product => product.Description).Should().BeEquivalentTo(expectedProductDescriptions);
        }

        [Test]
        public async Task FilterByDescriptionAndBrand_ReturnsProduct()
        {
            // arrange
            var repo = await CreateRepositoryWithProducts();

            // act
            var products = await repo.GetAllProducts(new ProductRepositoryFilter { Description = "Better", Brand = "Average" });

            // assert
            products.Should().HaveCount(1);
            products.First().Description.Should().Be("Better product");
        }


        private async Task<IProductRepository> CreateRepositoryWithProducts()
        {
            var repo = new ProductRepository();

            var product1 = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Description = "Super product")
                .And(entity => entity.Brand = "Reliable brand")
                .And(entity => entity.Model = "Top of the line")
                .And(entity => entity.Id = "ignorableId")
                .Build();
            var product2 = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Description = "Average product")
                .And(entity => entity.Brand = "Average brand")
                .And(entity => entity.Model = "Average model")
                .And(entity => entity.Id = "ignorableId")
                .Build();
            var product3 = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Description = "Better product")
                .And(entity => entity.Brand = "Average brand")
                .And(entity => entity.Model = "Average model")
                .And(entity => entity.Id = "ignorableId")
                .Build();

            await repo.CreateProduct(product1);
            await repo.CreateProduct(product2);
            await repo.CreateProduct(product3);

            return repo;
        }
    }
}
