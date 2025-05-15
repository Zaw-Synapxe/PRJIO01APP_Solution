using System.ComponentModel.DataAnnotations;

namespace Invoice.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
