using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestExample.Contracts
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts(ProductFilter productFilter);
        Task<Product> GetProduct(string id);
        Task<string> CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(string id);
    }
}
