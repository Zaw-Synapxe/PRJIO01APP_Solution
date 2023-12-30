using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XYZ.API.Service;

namespace XYZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IRepository<Category> _categoryRepository;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IRepository<Category> categoryRepository, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll() // GetAllCategory
        {
            _logger.LogInformation("This is Get All Async: Category");
            return Ok(await _categoryRepository.GetAllAsync());
        }






        //
    }
}
