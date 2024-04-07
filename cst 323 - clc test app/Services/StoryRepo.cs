using cst_323___clc_test_app.Models;
using MySqlConnector;

namespace cst_323___clc_test_app.Services
{
    public class StoryRepo : StoryService
    {
        private const string connectionString = "server=127.0.0.1;uid=root;pwd=root;database=cc-clc";
        private MySqlConnector.MySqlConnection conn = new MySqlConnector.MySqlConnection();
        private readonly ILogger<StoryRepo> _logger;

        public StoryRepo(ILogger<StoryRepo> logger)
        {
            _logger = logger;
        }

        // static string connectionString = RDSConnector.GetRDSConnectionString();
        // private MySqlConnection conn = new MySqlConnection(connectionString);
        public void AddStory(Story story)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    _logger.LogInformation("Entered AddStory from StoryRepo and connected to database for adding story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);

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
                    _logger.LogInformation("Story added successfully at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while adding story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting AddStory method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
        }
        public void DeleteStory(int id)
        {
            try
            {
                _logger.LogInformation("Entered DeleteStory from StoryRepo and connected to database for deleting story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM stories WHERE id = @id";

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = query;
                    comm.Parameters.Add(new MySqlParameter("@id", id));

                    comm.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while deleting story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting DeleteStory method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
        }

        public List<Story> GetAllStories()
        {
            List<Story> stories = new List<Story>();

            try
            {
                _logger.LogInformation("Entered GetAllStories from StoryRepo and connected to database for retrieving all stories at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

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
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while retrieving all stories at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting GetAllStories method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }

            return stories;
        }

        public List<Story> GetStoriesByAuthor(int authorId)
        {
            List<Story> stories = new List<Story>();

            try
            {
                _logger.LogInformation("Entered GetStoriesByAuthor from StoryRepo and connected to database to retrieve stories by user at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

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
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve story. An error occurred while retrieving a story by user at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting GetStoriesByAuthor method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }

            return stories;
        }

        public Story GetStory(int id)
        {
            Story story = new Story();

            try
            {
                _logger.LogInformation("Entered GetStory from StoryRepo and connected to database to retrieve a story {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

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
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve story. An error occurred while retrieving a story at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting GetStory method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }

            return story;
        }

        public bool UpdateStory(Story storyToUpdate)
        {
            try
            {
                _logger.LogInformation("Entered UpdateStory from StoryRepo and connected to database to update a story {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE stories SET userID = @userID, title = @title, premise = @premise, genre = @genre, story = @story WHERE id = @id";

                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = query;
                    comm.Parameters.Add(new MySqlParameter("@userID", storyToUpdate.userID));
                    comm.Parameters.Add(new MySqlParameter("@title", storyToUpdate.title));
                    comm.Parameters.Add(new MySqlParameter("@premise", storyToUpdate.premise));
                    comm.Parameters.Add(new MySqlParameter("@genre", storyToUpdate.genre));
                    comm.Parameters.Add(new MySqlParameter("@story", storyToUpdate.story));
                    comm.Parameters.Add(new MySqlParameter("@id", storyToUpdate.id));

                    int result = comm.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update story. An error occurred at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return false;
            }
            finally
            {
                _logger.LogInformation("Exiting UpdateStory method from StoryRepo {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
        }
    }
}