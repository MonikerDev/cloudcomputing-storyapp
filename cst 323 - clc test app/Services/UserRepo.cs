using cst_323___clc_test_app.Models;
using MySqlConnector;

namespace cst_323___clc_test_app.Services
{
    public class UserRepo : UserService
    {
        private const string connectionString = "server=saclc.mysql.database.azure.com;uid=moniker;pwd=Dickcock69;database=clcfinal";
        private MySqlConnector.MySqlConnection conn = new MySqlConnector.MySqlConnection();

        //   static string connectionString = RDSConnector.GetRDSConnectionString();
        //        private MySqlConnection conn = new MySqlConnection(connectionString);

        private readonly ILogger<UserRepo> _logger;

        public UserRepo(ILogger<UserRepo> logger)
        {
            _logger = logger;
        }


        #region Queries

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine("Connected");

                string query = "SELECT * FROM users";

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = query;


                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        id = reader.GetInt32("id"),
                        email = reader.GetString("email"),
                        password = reader.GetString("password")
                    });
                }

                conn.Close();


            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = new User();

            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine("Connected");

                string query = "SELECT * FROM users WHERE id = @id";

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = query;
                comm.Parameters.Add(new MySqlParameter("@id", id));

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    user = new User
                    {
                        id = reader.GetInt32("id"),
                        email = reader.GetString("email"),
                        password = reader.GetString("password")
                    };
                }

                conn.Close();


            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return user;
        }

        #endregion

        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                _logger.LogInformation("Entered GetUserByEmailAndPassword from UserRepo and attempting to get user by email and password: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", email, DateTime.UtcNow);
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM users WHERE Email = @Email AND Password = @Password";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                User user = new User
                                {
                                    id = reader.GetInt32("Id"),
                                    email = reader.GetString("Email"),
                                    // Other properties as needed
                                };
                                _logger.LogInformation("User found by email and password: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", email, DateTime.UtcNow);
                                return user;
                            }
                        }
                    }
                }
                _logger.LogInformation("User not found by email and password: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", email, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by email and password: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", email, DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting GetUserByEmailAndPassword method from UserRepo at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }

            return null; // User not found
        }

        public void AddUser(User newUser)
        {
            try
            {
                _logger.LogInformation("Entered AddUser from UserRepo and attempting to add user: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", newUser.email, DateTime.UtcNow);
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO users (Email, Password) VALUES (@Email, @Password)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", newUser.email);
                        cmd.Parameters.AddWithValue("@Password", newUser.password);

                        cmd.ExecuteNonQuery();
                        _logger.LogInformation("User added successfully: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", newUser.email, DateTime.UtcNow);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", newUser.email, DateTime.UtcNow);
            }
            finally
            {
                _logger.LogInformation("Exiting AddUser method from UserRepo at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
            }
        }
    }
}