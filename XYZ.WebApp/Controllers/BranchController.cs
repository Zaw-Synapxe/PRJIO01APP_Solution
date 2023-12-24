using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using XYZ.WebApp.APIClientService;

namespace XYZ.WebApp.Controllers
{
    public class BranchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAPIClientService<Branch> _iAPIClientService;

        public BranchController(ILogger<HomeController> logger, 
            IAPIClientService<Branch> iAPIClientService)
        {
            _logger = logger;
            _iAPIClientService = iAPIClientService;
        }

        //
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Requesting to Branch...");

            string _subURL = "Branch/GetAll";
            var members = await _iAPIClientService.GetAll(_subURL);
            return View(members);
        }

        //
        public async Task<IActionResult> Details(Int64 id)
        {
            _logger.LogInformation("Requesting to Branch Details...");

            string _subURL = "Branch/GetById/";
            var _Branch = await _iAPIClientService.GetById(id, _subURL);
            return View(_Branch);
        }

        //
        public IActionResult Create()
        {
            _logger.LogInformation("Requesting to Branch Create...");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Requesting to Branch Create (Insert New Record) ...");

                string _subURL = "Branch/Add";
                var resut = await _iAPIClientService.Add(branch, _subURL);
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        //
        public async Task<IActionResult> Edit(Int64 id)
        {
            _logger.LogInformation("Requesting to Branch Edit...");

            string _subURL = "Branch/GetById/";
            var _Branch = await _iAPIClientService.GetById(id, _subURL);
            return View(_Branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Branch branch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Requesting to Branch Edit (Update Record) ...");

                    string _subURL = "Branch/Update";
                    var resut = await _iAPIClientService.Update(branch, _subURL);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Message : " + ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        //
        public async Task<IActionResult> Delete(Int64 id)
        {
            _logger.LogInformation("Requesting to Branch Delete...");

            string _subURL = "Branch/GetById/";
            var _Branch = await _iAPIClientService.GetById(id, _subURL);
            return View(_Branch);
        }

        [HttpDelete, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(Int64 id)
        {
            _logger.LogInformation("Requesting to Branch Deleting Id - " + id + " ...");

            string _subURL = "Branch/Delete/";
            var resut = await _iAPIClientService.Delete(id, _subURL);
            return RedirectToAction(nameof(Index));
        }


        //
    }
}
