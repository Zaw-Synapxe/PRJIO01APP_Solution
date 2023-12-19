using Microsoft.AspNetCore.Mvc;

namespace FluentEmailImp.WebApp.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
