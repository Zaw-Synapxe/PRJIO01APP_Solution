using DapperCRUD.API.Domain;
using DapperCRUD.API.Repositories;

namespace DapperCRUD.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<List<Product>> GetAllProductsByNameAsync(string prodname)
        {
            return await _productRepository.GetAllProductByNameAsync(prodname);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<int> DeleteProductAsync(Product product)
        {
            return await _productRepository.DeleteAsync(product);
        }
    }
}
