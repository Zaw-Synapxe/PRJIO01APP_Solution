using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevPrj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeveloperController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> GetPopularDevelopers([FromQuery] int count)
        {
            var popularDevelopers = await _unitOfWork.Developers.GetPopularDevelopers(count);
            return Ok(popularDevelopers);

        }

        [HttpPost]
        public async Task<IActionResult> AddDeveloperAndProject()
        {
            var developer = new Developer
            {
                Followers = 35,
                Name = "Mukesh Murugan"
            };
            var project = new Project
            {
                Name = "codewithmukesh"
            };

            var resultD = await _unitOfWork.Developers.AddAsync(developer);
            var resultP = await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Developers.GetAllAsync();
            return Ok(result);
        }

        //
    }
}
