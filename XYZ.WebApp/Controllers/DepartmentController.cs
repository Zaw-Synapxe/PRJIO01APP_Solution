using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using XYZ.WebApp.APIClientService;

namespace XYZ.WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAPIClientService<Department> _iAPIClientService;

        public DepartmentController(ILogger<HomeController> logger,
            IAPIClientService<Department> iAPIClientService)
        {
            _logger = logger;
            _iAPIClientService = iAPIClientService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Requesting to Department...");

            string _subURL = "Department/GetAll";
            var members = await _iAPIClientService.GetAll(_subURL);
            return View(members);
        }




        //
    }
}
