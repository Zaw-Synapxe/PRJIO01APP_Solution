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

        //https://github.com/shahedbd/customobjectmapper-/blob/main/PersonalDataMNG/Controllers/CategoriesController.cs
        public CategoriesController(IRepository<Category> categoryRepository, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll() // GetAllCategory
        {
            _logger.LogInformation("This is Get All Async: Categories");
            return Ok(await _categoryRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<Category>> GetById(Int64 id)
        {
            _logger.LogInformation("This is GetById Async: Category : " + id);
            Category result = await _categoryRepository.GetByIdAsync(id);

            if (result == null)
            {
                return Ok("Not Found");
            }
            return Ok(result);

        }

        [HttpGet]
        [Route("GetByName/{categoryName}")]
        public async Task<ActionResult<Category>> GetByName(string categoryName)
        {
            return Ok(await _categoryRepository.FindByConditionAsync(x => x.Name == categoryName));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            _categoryRepository.Add(category);
            return Ok(await _categoryRepository.SaveChangesAsync());
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            _categoryRepository.Update(category);
            return Ok(await _categoryRepository.SaveChangesAsync());
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            await _categoryRepository.Delete(id);
            return Ok(await _categoryRepository.SaveChangesAsync());
        }


        //
    }
}
