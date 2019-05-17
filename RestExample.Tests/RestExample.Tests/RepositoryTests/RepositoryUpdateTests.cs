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
    public class RepositoryUpdateTests
    {
        [Test]
        public async Task ProductDescription_CanBeUpdated()
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
            product.Description = "A new description";
            await repo.UpdateProduct(product);

            var updatedProduct = await repo.GetProduct(id);

            // assert
            id.Should().NotBe("ignorableId");
            updatedProduct.Description.Should().Be("A new description");
            updatedProduct.Brand.Should().Be("Reliable brand");
            updatedProduct.Model.Should().Be("Top of the line");
        }

        [Test]
        public async Task UpdatingUnknownProduct_ThrowsException()
        {
            // arrange
            var repo = CreateRepository();
            var product = Builder<ProductEntity>.CreateNew()
                .With(entity => entity.Id = "unknown")
                .Build();

            // act
            Func<Task> fn = async () => await repo.UpdateProduct(product);

            // assert
            fn.Should().Throw<KeyNotFoundException>();
        }

        private IProductRepository CreateRepository()
        {
            return new ProductRepository();
        }
    }
}
