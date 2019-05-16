using RestExample.Model;

namespace RestExample.Contracts
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}