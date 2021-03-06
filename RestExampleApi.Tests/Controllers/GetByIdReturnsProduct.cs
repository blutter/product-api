﻿using System.Threading.Tasks;
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
    public class GetByIdReturnsProduct : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

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
        public void ThenTheProductIsReadFromTheService()
        {
            SUT.ProductService.Verify(repo => repo.GetProduct("id"), Times.Once);
        }

        [Test]
        public void ThenTheProductIsReturned()
        {
            var result = _result as OkNegotiatedContentResult<ProductResponse>;
            result.Should().NotBeNull();
            result.Content.Id.Should().Be("newId");
        }
    }
}
