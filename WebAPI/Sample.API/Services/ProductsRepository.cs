using Sample.API.Models;

namespace Sample.API.Services
{
    public class ProductsRepository
    {
        ProductsDb _db;

        //private bool shouldConnect = false;
        //private DateTime _connectTime = DateTime.Now;

        public ProductsRepository()
        {
            _db = new ProductsDb();
        }

        public List<Product> GetProducts() => _db;
    }
}
