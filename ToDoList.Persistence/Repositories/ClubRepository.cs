using ToDoList.Application.Interfaces.Repositories;
using ToDoList.Domain.Entities;

namespace ToDoList.Persistence.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly IGenericRepository<Club> _repository;

        public ClubRepository(IGenericRepository<Club> repository)
        {
            _repository = repository;
        }
    }
}
