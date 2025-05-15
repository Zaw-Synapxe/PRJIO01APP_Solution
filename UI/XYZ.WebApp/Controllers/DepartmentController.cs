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

        public async Task<IActionResult> Details(Int64 id)
        {
            _logger.LogInformation("Requesting to Department Details...");

            string _subURL = "Department/GetById/";
            var _Department = await _iAPIClientService.GetById(id, _subURL);
            return View(_Department);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Requesting to Department Create...");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department Department)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Requesting to Department Create (Insert New Record) ...");

                string _subURL = "Department/Add";
                var resut = await _iAPIClientService.Add(Department, _subURL);
                return RedirectToAction(nameof(Index));
            }
            return View(Department);
        }

        public async Task<IActionResult> Edit(Int64 id)
        {
            _logger.LogInformation("Requesting to Department Edit...");

            string _subURL = "Department/GetById/";
            var _Department = await _iAPIClientService.GetById(id, _subURL);
            return View(_Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Department Department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Requesting to Department Edit (Update Record) ...");

                    string _subURL = "Department/Update";
                    var resut = await _iAPIClientService.Update(Department, _subURL);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Message : " + ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Department);
        }

        public async Task<IActionResult> Delete(Int64 id)
        {
            _logger.LogInformation("Requesting to Department Delete...");

            string _subURL = "Department/GetById/";
            var _Department = await _iAPIClientService.GetById(id, _subURL);
            return View(_Department);
        }

        [HttpDelete, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(Int64 id)
        {
            _logger.LogInformation("Requesting to Department Deleting Id - " + id + " ...");

            string _subURL = "Department/Delete/";
            var resut = await _iAPIClientService.Delete(id, _subURL);
            return RedirectToAction(nameof(Index));
        }


        //
    }
}
