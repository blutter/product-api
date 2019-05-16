using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExample.Tests.RepositoryTests
{
    public class RepositoryTests
    {
        [Test]
        public async Task NewlyCreatedProduct_CanbeRead()
        {
            // arrange
            var repo = CreateRepository();
            var product = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Description = "Super product")
                .And(entity => entity.Brand = "Reliable brand")
                .And(entity => entity.Model = "Top of the line")
                .And(entity => entity.Id = "ignorableId")
                .Build();

            var id = await repo.CreateProduct(product);

            // act
            var readProduct = await repo.GetProduct(id);

            // assert
            id.Should().NotBe("ignorableId");
            readProduct.Description.Should().Be("Super product");
            readProduct.Brand.Should().Be("Reliable brand");
            readProduct.Model.Should().Be("Top of the line");
        }

        [Test]
        public async Task NewlyCreatedProducts_CanbeRead()
        {
            // arrange
            var repo = CreateRepository();
            var products = Builder<ProductEntity>.CreateListOfSize(2)
                .Build();

            var productIds = new List<string>();
            foreach (var product in products)
            {
                productIds.Add(await repo.CreateProduct(product));
            }

            // act
            var readProducts = await repo.GetAllProducts();

            // assert
            readProducts.Should().HaveCount(2);
            readProducts.Should().Contain(product => product.Id == productIds.First());
            readProducts.Should().Contain(product => product.Id == productIds.Skip(1).First());
        }

        [Test]
        public async Task CreatedProduct_CanBeDeleted()
        {
            // arrange
            var repo = CreateRepository();
            var product = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Description = "Super product")
                .And(entity => entity.Brand = "Reliable brand")
                .And(entity => entity.Model = "Top of the line")
                .And(entity => entity.Id = "ignorableId")
                .Build();

            var id = await repo.CreateProduct(product);

            // act
            await repo.DeleteProduct(id);
            var repoProduct = await repo.GetProduct(id);

            // assert
            repoProduct.Should().BeNull();
        }

        [Test]
        public async Task GettingAnUnknownProduct_ReturnsNull()
        {
            // arrange
            var repo = CreateRepository();

            // act
            var repoProduct = await repo.GetProduct("unknown product");

            // assert
            repoProduct.Should().BeNull();
        }

        [Test]
        public async Task DeletingUnknownProduct_ThrowsException()
        {
            // arrange
            var repo = CreateRepository();

            // act
            Func<Task> fn = async () => await repo.DeleteProduct("unknown product");

            // assert
            fn.Should().Throw<KeyNotFoundException>();
        }


        private IProductRepository CreateRepository()
        {
            return new ProductRepository();
        }
    }
}
