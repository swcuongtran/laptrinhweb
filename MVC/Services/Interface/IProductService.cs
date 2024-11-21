using MVC.Models;

namespace MVC.Services.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> SearchProducts(string searchQuery, int? categoryID);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> GetFeaturedProducts(int take);
        IEnumerable<Product> GetLatestProducts(int take);
    }
}
