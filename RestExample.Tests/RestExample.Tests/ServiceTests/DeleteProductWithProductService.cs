using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using SpecsFor.StructureMap;

namespace RestExample.Tests.ServiceTests
{
    public class DeleteProductWithProductService : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;

        protected override void When()
        {
            _productService = SUT.BuildProductService();

            var task = Task.Run(async () =>
            {
                await _productService.DeleteProduct("idToRemove").ConfigureAwait(false);
            });
            
            task.Wait();
        }

        [Test]
        public void ThenTheProductIsDeletedInTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.DeleteProduct("idToRemove"), Times.Once);
        }
    }
}
