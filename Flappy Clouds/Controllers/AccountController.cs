using System.Security.Claims;
using Flappy_Clouds.Entities;
using Flappy_Clouds.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Flappy_Clouds.Controllers
{
    public class AccountController : Controller
    {
        private readonly FlappyCloudsContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(FlappyCloudsContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(model);
                }

                User account = GetAccount(model);
                account.PasswordHash = _passwordHasher.HashPassword(account, model.Password);
                account.Address = model.Address;
                account.PhoneNumber = model.PhoneNumber;

                try
                {
                    _context.Users.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully.";
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "An error occurred while registering the user.");
                    return View(model);
                }

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new(ClaimTypes.Email, user.Email),
                            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                            new(ClaimTypes.Role, user.Role) 
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddHours(2)
                            });

                        return RedirectToAction("Index","Home");
                    }
                }

                ModelState.AddModelError("", "Username/Email or Password is incorrect.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult AddReview(ReviewViewModel model)
        //{
        //    // Logic to add review


        private static User GetAccount(RegistrationViewModel model)
        {
            return new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }
    }
}
