using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestExample.Model;

namespace RestExample.Contracts
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<string, ProductEntity> _dictionary = new ConcurrentDictionary<string, ProductEntity>();

        public Task<string> CreateProduct(ProductEntity product)
        {
            var id = Guid.NewGuid().ToString();
            product.Id = id;

            _dictionary.TryAdd(id, product);

            return Task.FromResult(id);
        }

        public Task DeleteProduct(string id)
        {
            if (!_dictionary.TryRemove(id, out ProductEntity deletedProduct))
            {
                throw new KeyNotFoundException($"Could not delete product id {id} - product not found");
            }

            return Task.CompletedTask;
        }

        public Task<List<ProductEntity>> GetAllProducts()
        {
            var repositorySnapshot = _dictionary.ToArray();

            var productList = repositorySnapshot.Select(kvp => kvp.Value).ToList();

            return Task.FromResult(productList);
        }

        public Task<ProductEntity> GetProduct(string id)
        {
            ProductEntity product = null;
            _dictionary.TryGetValue(id, out product);

            return Task.FromResult(product);
        }

        public Task UpdateProduct(ProductEntity product)
        {
            _dictionary.AddOrUpdate(product.Id,
                (_) => { throw new KeyNotFoundException(); },
                (id, oldValue) =>
                {
                    product.Id = id;
                    product.Description = product.Description ?? oldValue.Description;
                    product.Brand = product.Brand ?? oldValue.Brand;
                    product.Model = product.Model ?? oldValue.Model;

                    return product;
                });

            return Task.CompletedTask;
        }
    }
}
