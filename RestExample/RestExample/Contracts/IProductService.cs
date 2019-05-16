using System.Collections.Generic;

namespace RestExample.Contracts
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
