namespace Sample.API.Models
{
    public class ProductsDb : List<Product>
    {
        public ProductsDb()
        {

            Add(new Product() { ProductId = 1, ProductName = "Laptop", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 2, ProductName = "Hard Disk", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 3, ProductName = "RAM", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 4, ProductName = "Printer", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 5, ProductName = "SSD", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 6, ProductName = "USB", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 7, ProductName = "Laptop Charger", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 8, ProductName = "DVD ROM", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 9, ProductName = "Mouse", Price = 340000, Url = "" });
            Add(new Product() { ProductId = 10, ProductName = "Keyboard", Price = 340000, Url = "" });
        }
    }
}
