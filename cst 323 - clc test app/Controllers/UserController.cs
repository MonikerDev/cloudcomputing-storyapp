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
        private ILogger<StoryController> _logger;
        private readonly UserService _usersRepo;
        public UserController(ILogger<StoryController> logger, UserService userRepo)
        {
            _logger = logger;
            _usersRepo = userRepo;
        }

            public IActionResult Index()
            {
                _logger.LogInformation("Entering Index method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Visiting index and fetching all users at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                var users = _usersRepo.GetAllUsers();
                _logger.LogInformation("Exiting Index method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View(users);
            }

            public IActionResult Login()
            {
                _logger.LogInformation("Entering Login method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Rendering login page at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Exiting Login method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(LoginCredentials model)
            {
                _logger.LogInformation("Entering Login (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Attempting login for user with email: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", model.email, DateTime.UtcNow);

                if (ModelState.IsValid)
                {
                    var user = _usersRepo.GetUserByEmailAndPassword(model.email, model.password);
                    if (user != null)
                    {
                        var claims = new[]
                        {
                        new Claim(ClaimTypes.Name, user.email),
                    };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties();

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity),
                            authProperties);

                        _logger.LogInformation("User {Email} successfully authenticated at {DateTime:yyyy-MM-dd HH:mm:ss}", model.email, DateTime.UtcNow);

                        _logger.LogInformation("Exiting Login (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                        return RedirectToAction("Index", "Home");
                    }

                    _logger.LogWarning("Invalid login attempt for user with email: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", model.email, DateTime.UtcNow);
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }

                _logger.LogInformation("Exiting Login (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View(model);
            }

            public IActionResult Register()
            {
                _logger.LogInformation("Entering Register method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Rendering registration page at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Exiting Register method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View();
            }

            [HttpPost]
            public IActionResult Register(RegisterCredentials model)
            {
                _logger.LogInformation("Entering Register (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Attempting to register new user with email: {Email} at {DateTime:yyyy-MM-dd HH:mm:ss}", model.email, DateTime.UtcNow);

                if (ModelState.IsValid)
                {
                    var newUser = new User
                    {
                        email = model.email,
                        password = model.password,
                    };
                    _usersRepo.AddUser(newUser);
                    _logger.LogInformation("User {Email} registered successfully at {DateTime:yyyy-MM-dd HH:mm:ss}", model.email, DateTime.UtcNow);
                    _logger.LogInformation("Exiting Register (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                    return RedirectToAction("Login");
                }

                _logger.LogInformation("Exiting Register (HttpPost) method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return View(model);
            }

            public async Task<IActionResult> SignOut()
            {
                _logger.LogInformation("Entering SignOut method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                _logger.LogInformation("Signing out user at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation("Exiting SignOut method in UserController at {DateTime:yyyy-MM-dd HH:mm:ss}", DateTime.UtcNow);
                return RedirectToAction("Index", "Home");
            }
        }
    }