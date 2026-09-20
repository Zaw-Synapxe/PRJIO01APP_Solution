using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HudAsp.Models;

namespace HudAsp.Controllers;

public class AiController : Controller
{
		public IActionResult Chat()
		{
				return View();
		}

		public IActionResult ImageGenerator()
		{
				return View();
		}
}
