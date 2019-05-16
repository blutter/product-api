using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using RestExample.Contracts;
using RestExample.Model;
using SpecsFor.StructureMap;

namespace RestExample.Tests.ServiceTests
{
    public class UpdateProductWithProductService : SpecsFor<ProductServiceFactory>
    {
        private IProductService _productService;
        private Product _product;

        protected override void When()
        {
            _productService = SUT.BuildProductService();
            _product = Builder<Product>.CreateNew()
                .With(p => p.Description = "fancy product")
                .Build();

            var task = Task.Run(async () =>
            {
                await _productService.UpdateProduct(_product).ConfigureAwait(false);
            });
            
            task.Wait();
        }

        [Test]
        public void ThenTheProductsAreUpdatedInTheRepository()
        {
            SUT.ProductRepository.Verify(repo => repo.UpdateProduct(It.Is<ProductEntity>(entity => entity.Description == "fancy product")), Times.Once);
        }
    }
}
