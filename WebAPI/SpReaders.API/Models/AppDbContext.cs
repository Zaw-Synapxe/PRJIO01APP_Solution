using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpReaders.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string OIdProvider { get; set; }
        public string OId { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
    }

}
