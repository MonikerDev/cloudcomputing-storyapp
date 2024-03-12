using cst_323___clc_test_app.Models;
using MySqlConnector;

namespace cst_323___clc_test_app.Services
{
	public class StoryRepo : StoryService
	{
        // private const string connectionString = "server=127.0.0.1;uid=root;pwd=root;database=cc-clc";
        // private MySqlConnector.MySqlConnection conn = new MySqlConnector.MySqlConnection();
        static string connectionString = RDSConnector.GetRDSConnectionString();
        private MySqlConnection conn = new MySqlConnection(connectionString);

        public void AddStory(Story story)
		{
			try
			{
				conn.ConnectionString = connectionString;
				conn.Open();
				Console.WriteLine("Connected");

				string query = "INSERT INTO stories (userID, title, premise, genre, story) " +
					"VALUES (@userID, @title, @premise, @genre, @story)";

				MySqlCommand comm = conn.CreateCommand();
				comm.CommandText = query;
				comm.Parameters.Add(new MySqlParameter("@userID", story.userID));
				comm.Parameters.Add(new MySqlParameter("@title", story.title));
				comm.Parameters.Add(new MySqlParameter("@premise", story.premise));
				comm.Parameters.Add(new MySqlParameter("@genre", story.genre));
				comm.Parameters.Add(new MySqlParameter("@story", story.story));

				comm.ExecuteNonQuery();

				conn.Close();
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
		}

		public void DeleteStory(int id)
		{
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine("Connected");

                string query = "DELETE FROM stories WHERE id = @id";

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = query;
				comm.Parameters.Add(new MySqlParameter("@id", id));

				comm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

		public List<Story> GetAllStories()
		{
			List<Story> stories = new List<Story>();

			try
			{
				conn.ConnectionString = connectionString;
				conn.Open();
				Console.WriteLine("Connected");

				string query = "SELECT * FROM stories";

				MySqlCommand comm = conn.CreateCommand();
				comm.CommandText = query;

				
				MySqlDataReader reader = comm.ExecuteReader();

				while (reader.Read())
				{
					stories.Add(new Story
					{
						id = reader.GetInt32("id"),
						userID = reader.GetInt32("userID"),
						title = reader.GetString("title"),
						premise = reader.GetString("premise"),
						genre = reader.GetString("genre"),
						story = reader.GetString("story")
					});
				}

				conn.Close();

				
			}
			catch (Exception e)
			{
				Console.Write(e);
			}

			return stories;
		}

        public List<Story> GetStoriesByAuthor(int authorId)
        {
            List<Story> stories = new List<Story>();

            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine("Connected");

                string query = "SELECT * FROM stories WHERE userID = @userID";

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = query;
				comm.Parameters.Add(new MySqlParameter("@userID", authorId));

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    stories.Add(new Story
                    {
                        id = reader.GetInt32("id"),
                        userID = reader.GetInt32("userID"),
                        title = reader.GetString("title"),
                        premise = reader.GetString("premise"),
                        genre = reader.GetString("genre"),
                        story = reader.GetString("story")
                    });
                }

                conn.Close();


            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return stories;
        }

        public Story GetStory(int id)
		{
			Story story = new Story();

            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine("Connected");

                string query = "SELECT * FROM stories WHERE id = @id";

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = query;
				comm.Parameters.Add(new MySqlParameter("@id", id));


                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    story = new Story
                    {
                        id = reader.GetInt32("id"),
                        userID = reader.GetInt32("userID"),
                        title = reader.GetString("title"),
                        premise = reader.GetString("premise"),
                        genre = reader.GetString("genre"),
                        story = reader.GetString("story")
                    };
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

			return story;
        }

		public bool UpdateStory(Story storyToUpdate)
		{
			try
			{
				conn.ConnectionString = connectionString;
				conn.Open();
				Console.WriteLine("Connected");

				string query = "UPDATE stories SET userID = @userID, title = @title, premise = @premise, genre = @genre, story = @story WHERE id = @id";

				Console.WriteLine(query);

				MySqlCommand comm = conn.CreateCommand();
				comm.CommandText = query;
				comm.Parameters.Add(new MySqlParameter("@userID", storyToUpdate.userID));
				comm.Parameters.Add(new MySqlParameter("@title", storyToUpdate.title));
				comm.Parameters.Add(new MySqlParameter("@premise", storyToUpdate.premise));
				comm.Parameters.Add(new MySqlParameter("@genre", storyToUpdate.genre));
				comm.Parameters.Add(new MySqlParameter("@story", storyToUpdate.story));
				comm.Parameters.Add(new MySqlParameter("@id", storyToUpdate.id));

				int result = comm.ExecuteNonQuery();
				Console.WriteLine(storyToUpdate);

				conn.Close();
				return true;
			}
			catch (Exception e)
			{
				Console.Write(e);
				return false;
			}
		}
	}
}
