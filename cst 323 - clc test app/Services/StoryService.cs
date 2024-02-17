using cst_323___clc_test_app.Models;

namespace cst_323___clc_test_app.Services
{
	public interface StoryService
	{
		public List<Story> GetAllStories();

		public Story GetStory(int id);

		public bool UpdateStory(Story storyToUpdate);

		public void DeleteStory(int id);

		public void AddStory(Story story);
	}
}
