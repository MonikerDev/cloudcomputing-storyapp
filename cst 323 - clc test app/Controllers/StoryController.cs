using cst_323___clc_test_app.Models;
using cst_323___clc_test_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace cst_323___clc_test_app.Controllers
{
	public class StoryController : Controller
	{
		public IActionResult Story()
		{
			Story story = new Story();
			story.genre = "Horror";
			story.premise = "Spaghetti is a deadly poison";
			story = StoryGenerator.WriteStory(story).Result;
			
			return View(story);
		}
	}
}
