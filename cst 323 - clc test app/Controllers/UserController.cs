using cst_323___clc_test_app.Models;
using cst_323___clc_test_app.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace cst_323___clc_test_app.Controllers
{
    public class UserController : Controller
    {
        private UserService usersRepo = new UserRepo();

        // Return all users from MySQL Database. 
        public IActionResult Index()
        {
            var users = usersRepo.GetAllUsers();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }


        // Check if a user exists and the password credentials are matching. If so, authenticate. 
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials model)
        {
            if (ModelState.IsValid)
            {
                var user = usersRepo.GetUserByEmailAndPassword(model.email, model.password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.email),
                    };

                    // Create identity from claims
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        // Add properties if needed
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(model);
        }



        public IActionResult Register()
        {
            return View();
        }



        // "Create User" 
        [HttpPost]
        public IActionResult Register(RegisterCredentials model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    email = model.email,
                    password = model.password,
                };
                usersRepo.AddUser(newUser);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
