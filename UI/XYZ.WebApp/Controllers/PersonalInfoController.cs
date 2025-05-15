using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using XYZ.WebApp.APIClientService;

namespace XYZ.WebApp.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAPIClientService<PersonalInfo> _iAPIClientService;

        public PersonalInfoController(ILogger<HomeController> logger,
            IAPIClientService<PersonalInfo> iAPIClientService )
        {
            _logger = logger;
            _iAPIClientService = iAPIClientService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Requesting to PersonalInfo...");

            string _subURL = "PersonalInfo/GetAll";
            var members = await _iAPIClientService.GetAll(_subURL);
            return View(members.Take(100));
        }
    }
}
