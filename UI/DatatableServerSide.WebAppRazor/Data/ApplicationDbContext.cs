using DatatableServerSide.WebAppRazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatatableServerSide.WebAppRazor.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public DbSet<Customer> tbl_Customers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Person> tbl_Persons { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DbSet<TestRegister> tbl_TestRegisters { get; set; }

    }
}
