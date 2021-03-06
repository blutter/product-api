﻿using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestExampleApi.Controllers;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class ProductIsSuccessfullyDeleted : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;
        private IHttpActionResult _result;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            var task = Task.Run(async () =>
            {
                _result = await _controller.Delete("id").ConfigureAwait(false);
            });

            task.Wait();
        }

        [Test]
        public void ThenTheProductIsDeletedUsingTheService()
        {
            SUT.ProductService.Verify(repo => repo.DeleteProduct("id"), Times.Once);
        }

        [Test]
        public void ThenAnOkResultIsReturned()
        {
            _result.Should().BeOfType<OkResult>();
        }
    }
}
