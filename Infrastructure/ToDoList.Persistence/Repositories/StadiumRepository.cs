using ToDoList.Application.Interfaces.Repositories;
using ToDoList.Domain.Entities;

namespace ToDoList.Persistence.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly IGenericRepository<Stadium> _repository;

        public StadiumRepository(IGenericRepository<Stadium> repository)
        {
            _repository = repository;
        }
    }
}
