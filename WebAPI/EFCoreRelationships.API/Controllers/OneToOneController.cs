using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Transactions;

namespace EFCoreRelationships.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneToOneController : ControllerBase
    {
        protected readonly AppDbContext _context;
        public OneToOneController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-users")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _context.Users.Include(x => x.Profile).ToListAsync());
        }

        [HttpPost("add-profile")]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            return Ok(await _context.Profiles.Include(x => x.User).ToListAsync());
        }



        //
    }

    // Model for One-to-One

    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public Profile? Profile { get; set; } //Navigation Property
    }

    public class Profile
    {
        public int Id { get; set; }
        public string? Bio { get; set; }
        public User? User { get; set; } //Navigation Property
        public int UserId { get; set; } //Foreign key 
        // this is unique key for One-to-One with User // index

    }

    //public partial class AppDbContext : DbContext
    //{

    //    public AppDbContext(DbContextOptions options) : base(options) { }

    //    public DbSet<User> Users { get; set; } = default!;
    //    public DbSet<Profile> Profiles { get; set; } = default!;
    //}

    // or

    public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
    }


    //
}
