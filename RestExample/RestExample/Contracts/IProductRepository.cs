using RestExample.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestExample.Contracts
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAllProducts();
        Task<ProductEntity> GetProduct(string id);
        Task<string> CreateProduct(ProductEntity product);
        Task UpdateProduct(ProductEntity product);
        Task DeleteProduct(string id);
    }
}
