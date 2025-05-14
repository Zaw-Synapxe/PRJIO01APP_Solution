using DapperCRUD.API.Domain;

namespace DapperCRUD.API.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<List<Product>> GetAllProductsByNameAsync(string prodname);
        public Task<Product> GetProductById(int id);
        public Task<int> CreateProductAsync(Product product);
        public Task<int> UpdateProductAsync(Product product);
        public Task<int> DeleteProductAsync(Product product);
    }
}
