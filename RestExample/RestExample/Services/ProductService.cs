﻿using AutoMapper;
using RestExample.Contracts;
using RestExample.Model;
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
            List<ProductEntity> products = null;

            if (productFilter == null || 
                (string.IsNullOrWhiteSpace(productFilter.Brand) && string.IsNullOrWhiteSpace(productFilter.Description) && string.IsNullOrWhiteSpace(productFilter.Model)))
            {
                products = await productRepository.GetAllProducts().ConfigureAwait(false);
            }
            else
            {
                products = await productRepository.GetAllProducts(mapper.Map<ProductRepositoryFilter>(productFilter));
            }

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
