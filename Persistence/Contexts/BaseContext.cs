using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {

        }

        //
    }
}
