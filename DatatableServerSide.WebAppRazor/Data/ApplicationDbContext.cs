using DatatableServerSide.WebAppRazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatatableServerSide.WebAppRazor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> tbl_Customers { get; set; }

    }
}
