using cst_323___clc_test_app.Models;
using cst_323___clc_test_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace cst_323___clc_test_app.Controllers
{
	public class StoryController : Controller
	{
		private StoryService storyRepo = new StoryRepo();

		public IActionResult Index()
		{	
			return View(storyRepo.GetAllStories());
		}

		public IActionResult CreateStory()
		{
			return View();
		}

		public IActionResult AddStory(Story story)
		{
			story.userID = 1;
			story = StoryGenerator.WriteStory(story).Result;
			storyRepo.AddStory(story);
			
			return View("Index", storyRepo.GetAllStories());
		}

		public IActionResult EditStory(int id)
		{
			Story story = storyRepo.GetStory(id);
			return View("EditStory", story);
		}

		public IActionResult ProcessUpdate(Story storyToUpdate)
		{
            Console.WriteLine(storyToUpdate.title);
            if (storyRepo.UpdateStory(storyToUpdate))
			{
				return View("Index", storyRepo.GetAllStories());
			}
			else
			{
				return View("EditStory", storyToUpdate);
			}
		}

		public IActionResult ShowSingleStory(int id)
		{
            Story story = storyRepo.GetStory(id);
            return View("Story", story);
		}

		public IActionResult DeleteStory(int id)
		{
			storyRepo.DeleteStory(id);

            return View("Index", storyRepo.GetAllStories());
        }
	}
}
