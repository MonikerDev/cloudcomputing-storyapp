using cst_323___clc_test_app.Models;
using cst_323___clc_test_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cst_323___clc_test_app.Controllers
{
//	[Authorize]
	public class StoryController : Controller
	{
        private readonly ILogger<StoryController> _logger;
        private readonly StoryService _storyRepo;
        private readonly IConfiguration _configuration;

        public StoryController(ILogger<StoryController> logger, StoryService storyRepo, IConfiguration configuration)
        {
            _logger = logger;
            _storyRepo = storyRepo;
	    _configuration = configuration;
            StoryGenerator.apiKey = configuration.GetSection("API_KEY").Value;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Entering Index method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Visiting index and getting all stories at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            var stories = _storyRepo.GetAllStories();
            _logger.LogInformation("Exiting Index method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return View(stories);
        }

        public IActionResult CreateStory()
        {
            _logger.LogInformation("Entering CreateStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Visited CreateStory from StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Exiting CreateStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return View();
        }

        public IActionResult AddStory(Story story)
        {
            _logger.LogInformation("Entering AddStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Adding new story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            story.userID = 1;
            story = StoryGenerator.WriteStory(story).Result;
            _storyRepo.AddStory(story);
            _logger.LogInformation("Exiting AddStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return RedirectToAction("Index");
        }

        public IActionResult EditStory(int id)
        {
            _logger.LogInformation("Entering EditStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Editing story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", id, DateTime.UtcNow);
            Story story = _storyRepo.GetStory(id);
            _logger.LogInformation("Exiting EditStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return View("EditStory", story);
        }

        [HttpPost]
        public IActionResult ProcessUpdate(Story storyToUpdate)
        {
            _logger.LogInformation("Entering ProcessUpdate method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Processing update for story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", storyToUpdate.id, DateTime.UtcNow);
            bool result = _storyRepo.UpdateStory(storyToUpdate);
            if (result)
            {
                _logger.LogInformation("Update successful for story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", storyToUpdate.id, DateTime.UtcNow);
                _logger.LogInformation("Exiting ProcessUpdate method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError("Failed to update story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", storyToUpdate.id, DateTime.UtcNow);
                _logger.LogInformation("Exiting ProcessUpdate method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View("EditStory", storyToUpdate);
            }
        }

        public IActionResult ShowSingleStory(int id)
        {
            _logger.LogInformation("Entering ShowSingleStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Viewing single story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", id, DateTime.UtcNow);
            Story story = _storyRepo.GetStory(id);
            _logger.LogInformation("Exiting ShowSingleStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return View("Story", story);
        }

        public IActionResult DeleteStory(int id)
        {
            _logger.LogInformation("Entering DeleteStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            _logger.LogInformation("Deleting story with ID {ID} at {DateTime:yyyy-MM-dd HH:mm:ss}", id, DateTime.UtcNow);
            _storyRepo.DeleteStory(id);
            _logger.LogInformation("Exiting DeleteStory method in StoryController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            return RedirectToAction("Index");
        }
    }
}
