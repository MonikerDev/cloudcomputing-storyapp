using cst_323___clc_test_app.Models;
using MySqlConnector;

namespace cst_323___clc_test_app.Services
{
    public class UserRepo : UserService
    {
        private const string connectionString = "server=127.0.0.1;uid=admin;pwd=yummy;database=cc-clc";
        private MySqlConnector.MySqlConnection conn = new MySqlConnector.MySqlConnection();

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
    }
}
