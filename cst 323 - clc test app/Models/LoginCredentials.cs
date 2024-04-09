using System.ComponentModel.DataAnnotations;

namespace cst_323___clc_test_app.Models
{
    public class LoginCredentials
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }

}
