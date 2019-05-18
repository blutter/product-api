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

        public Task<List<ProductEntity>> GetAllProducts(ProductRepositoryFilter filter)
        {
            var filteredProducts = _dictionary.ToArray().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                filteredProducts = filteredProducts.Where(kvp => 
                    !string.IsNullOrWhiteSpace(kvp.Value.Description) && kvp.Value.Description.Contains(filter.Description));
            }
            if (!string.IsNullOrWhiteSpace(filter.Brand))
            {
                filteredProducts = filteredProducts.Where(kvp =>
                    !string.IsNullOrWhiteSpace(kvp.Value.Brand) && kvp.Value.Brand.Contains(filter.Brand));
            }
            if (!string.IsNullOrWhiteSpace(filter.Model))
            {
                filteredProducts = filteredProducts.Where(kvp =>
                    !string.IsNullOrWhiteSpace(kvp.Value.Model) && kvp.Value.Model.Contains(filter.Model));
            }

            return Task.FromResult(filteredProducts.Select(kvp => kvp.Value).ToList());
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
                (_) => { throw new KeyNotFoundException($"Could not update product id {product.Id} - product not found"); },
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
