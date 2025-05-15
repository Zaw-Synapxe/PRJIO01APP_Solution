using System.ComponentModel.DataAnnotations;

namespace Invoice.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
    }
}
