using AutoMapper;
using RestExample.Contracts;
using RestExample.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestExample.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<string> CreateProduct(Product product)
        {
            var productEntity = mapper.Map<ProductEntity>(product);
            var id = await productRepository.CreateProduct(productEntity).ConfigureAwait(false);

            return id;
        }

        public Task DeleteProduct(string id)
        {
            return productRepository.DeleteProduct(id);
        }

        public async Task<List<Product>> GetAllProducts(ProductFilter productFilter)
        {
            var products = await productRepository.GetAllProducts().ConfigureAwait(false);

            return mapper.Map<List<Product>>(products);
        }

        public async Task<Product> GetProduct(string id)
        {
            var product = await productRepository.GetProduct(id).ConfigureAwait(false);

            return mapper.Map<Product>(product);
        }

        public Task UpdateProduct(Product product)
        {
            var productEntity = mapper.Map<ProductEntity>(product);

            return productRepository.UpdateProduct(productEntity);
        }
    }
}
