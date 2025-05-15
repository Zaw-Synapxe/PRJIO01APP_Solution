using SpReaders.API.Contracts.DTO;
using SpReaders.API.Models;

namespace SpReaders.API.Providers.Repositories
{
    public interface IUserRepository
    {
        Task<int> GetOrCreateUserAsync(UserProfile id);

        Task<List<UserRolesDTO>> GetUserRolesByProfileId(int profileId);
    }
}
