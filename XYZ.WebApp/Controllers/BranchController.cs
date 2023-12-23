using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using XYZ.WebApp.APIClientService;

namespace XYZ.WebApp.Controllers
{
    public class BranchController : Controller
    {
        private IAPIClientService<Branch> _iAPIClientService;

        public BranchController(IAPIClientService<Branch> iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<IActionResult> Index()
        {
            string _subURL = "Branch/GetAll";
            var members = await _iAPIClientService.GetAll(_subURL);
            return View(members);
        }

        public async Task<IActionResult> Details(Int64 id)
        {
            string _subURL = "Branch/GetById/";
            var _Branch = await _iAPIClientService.GetById(id, _subURL);
            return View(_Branch);
        }
        public IActionResult Create()
        {
            return View();
        }



        //
    }
}
