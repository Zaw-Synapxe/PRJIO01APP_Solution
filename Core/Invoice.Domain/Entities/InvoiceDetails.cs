using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Entities
{
    public class InvoiceDetails
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceInfoId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Qunatity { get; set; }
        public decimal Tax { get; set; }
        public double Total { get; set; }
    }
}
