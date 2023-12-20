using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace XYZ.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // How to fix the warning
        // Non-nullable property 'Accounts' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        // public DbSet<PersonalInfo> PersonalInfo { get; set; }

        public DbSet<PersonalInfo> tbl_PersonalInfo => Set<PersonalInfo>();

        //
    }
}
