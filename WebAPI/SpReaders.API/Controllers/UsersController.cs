using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpReaders.API.Models;
using SpReaders.API.Providers.Repositories;

namespace SpReaders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("GetOrCreate")]
        public async Task<IActionResult> GetOrCreate([FromBody] UserProfile id)
        {
            return Ok(await _repository.GetOrCreateUserAsync(id));
        }


        //
    }
}
