namespace cst_323___clc_test_app.Models
{
	public class User
	{
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<Story> stories { get; set; }
    }
}
