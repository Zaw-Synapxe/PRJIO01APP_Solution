using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HudAsp.Models;

namespace HudAsp.Controllers;

public class LandingController : Controller
{
		public IActionResult Index()
		{
				return View();
		}
}