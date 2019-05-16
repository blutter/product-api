using RestExample.Model;
using System.Collections.Generic;

namespace RestExample.Contracts
{
    public interface IProductRepository
    {
        List<ProductEntity> GetAllProducts();
        ProductEntity GetProduct(int id);
        void CreateProduct(ProductEntity product);
        void UpdateProduct(ProductEntity product);
        void DeleteProduct(int id);
    }
}
