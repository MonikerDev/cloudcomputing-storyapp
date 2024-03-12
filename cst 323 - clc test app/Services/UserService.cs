using cst_323___clc_test_app.Models;

namespace cst_323___clc_test_app.Services
{
    public interface UserService
    {
        public List<User> GetAllUsers();
        User GetUserByEmailAndPassword(string email, string password);
        void AddUser(User user);

        public User GetUserById(int id);
    }
}
