using EFCore.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ConsoleApp.Data
{
    public class EFCoreDbContext : DbContext
    {
        IConfiguration appConfig;

        public EFCoreDbContext(IConfiguration config)
        {
            appConfig = config;
        }

        ////Constructor calling the Base DbContext Class Constructor
        //public EFCoreDbContext() : base()
        //{
        //}


        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //To Display the Generated the Database Script
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            //Configuring the Connection String
            //optionsBuilder.UseSqlServer(@"Server=LAPTOP-6P5NK25R\SQLSERVER2022DEV;Database=EFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;");

            //string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            //optionsBuilder.UseSqlServer(@"Server=MAXIMUS-XI\\SQLDEV2017;Database=SQL2017_TESTDB2; User Id=sa; Password=ERPfegha1730; Trusted_Connection=false; MultipleActiveResultSets=true; Persist Security Info=True;");

            optionsBuilder.UseSqlServer(appConfig.GetConnectionString("DefaultConnection"));

            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // //Seeding Country Master Data using HasData method
            // modelBuilder.Entity<Country>().HasData(
            //    new() { CountryId = 1, CountryName = "India", CountryCode = "IND" },
            //    new() { CountryId = 2, CountryName = "Austrailla", CountryCode = "AUS" },
            //    new() { CountryId = 3, CountryName = "Singapore", CountryCode = "SIN" },
            //    new() { CountryId = 4, CountryName = "Myanmar", CountryCode = "MYA" }
            //);

            ////Seeding State Master Data using HasData method
            //modelBuilder.Entity<State>().HasData(
            //    new() { StateId = 1, StateName = "ODISHA", CountryId = 1 },
            //    new() { StateId = 2, StateName = "DELHI", CountryId = 1 },
            //    new() { StateId = 3, StateName = "SHAN", CountryId = 4 }
            //);

            // //Seeding City Master Data using HasData method
            // modelBuilder.Entity<City>().HasData(
            //     new() { CityId = 1, CityName = "Bhubaneswar", StateId = 1 },
            //     new() { CityId = 2, CityName = "Cuttack", StateId = 1 },
            //     new() { CityId = 3, CityName = "TaungGyi", StateId = 3 }
            //);
        }

        //older version <= 6.0
        //public DbSet<Customer> Customers => Set<Customer>();
        //public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Country> tbl_Country_MA { get; set; } = null!;
        public DbSet<State> tbl_State_MA { get; set; } = null!;
        public DbSet<City> tbl_City_MA { get; set; } = null!;
        
        public DbSet<Student> tbl_Student { get; set; } = null!;


        //Adding Domain Classes as DbSet Properties
        public DbSet<Student2> tbl_Student2 { get; set; } = null!;
        public DbSet<Standard> tbl_Standards { get; set; } = null!;


        // https://github.com/juldhais/InsertMillionRecords/blob/master/DataContext.cs
        public DbSet<Product> tbl_Products { get; set; } = null!;
        //

    }
}
