using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

using RestExampleApi.Controllers;
using SpecsFor.StructureMap;

namespace RestExampleApi.Tests.Controllers
{
    public class ProductIsSuccessfullyDeleted : SpecsFor<ProductsControllerFactory>
    {
        private ProductsController _controller;

        protected override void When()
        {
            _controller = SUT.BuildProductController();

            var task = Task.Run(async () =>
            {
                await _controller.Delete("id").ConfigureAwait(false);
            });

            task.Wait();
        }

        [Test]
        public void ThenTheProductIsDeletedUsingTheService()
        {
            SUT.ProductService.Verify(repo => repo.DeleteProduct("id"), Times.Once);
        }
    }
}
