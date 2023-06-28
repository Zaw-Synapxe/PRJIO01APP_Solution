using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Developer>> GetPopularDevelopers(int count)
        {
            return await _context.Developers.OrderByDescending(d => d.Followers).Take(count).ToListAsync();
        }

        //
    }
}
