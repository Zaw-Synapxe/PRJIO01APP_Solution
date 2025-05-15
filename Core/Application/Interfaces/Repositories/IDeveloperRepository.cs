using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDeveloperRepository : IGenericRepository<Developer>
    {
        Task<IEnumerable<Developer>> GetPopularDevelopers(int count);
    }
}
