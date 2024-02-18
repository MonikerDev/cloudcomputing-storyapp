namespace cst_323___clc_test_app.Models
{
	public class Story
	{
        public int id { get; set; }
        public int userID { get; set; }
        public string title { get; set; }
        public string premise { get; set; }
        public string genre { get; set; }
        public string story { get; set; }

		public Story() { }

		public Story(int id, int userID, string title, string premise, string genre, string story)
		{
			this.id = id;
			this.userID = userID;
			this.title = title;
			this.premise = premise;
			this.genre = genre;
			this.story = story;
		}

        public override string ToString()
        {
			return "id: " + id + "\ntitle: " + title + "\npremise: " + premise;
        }
    }
}
