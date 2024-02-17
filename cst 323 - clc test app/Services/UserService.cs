using cst_323___clc_test_app.Models;

namespace cst_323___clc_test_app.Services
{
    public interface UserService
    {
        public List<User> GetAllUsers();

        public User GetUserById(int id);
    }
}
