using Microsoft.AspNetCore.Mvc;

namespace cst_323___clc_test_app.Controllers
{
	public class StoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
