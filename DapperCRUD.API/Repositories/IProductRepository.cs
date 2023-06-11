using DapperCRUD.API.Domain;

namespace DapperCRUD.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllProductByNameAsync(string prodname);
    }
}
