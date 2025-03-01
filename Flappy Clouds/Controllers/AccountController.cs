using System.Security.Claims;
using Flappy_Clouds.Entities;
using Flappy_Clouds.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Flappy_Clouds.Controllers
{
    public class AccountController(FlappyCloudsContext context, IPasswordHasher<User> passwordHasher) : Controller
    {

        private readonly FlappyCloudsContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync(); // Ensure the model is never null
            return View(users);
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                User account = GetAccount(model);
                account.PasswordHash = _passwordHasher.HashPassword(account, model.Password);
                account.Address = model.Address;
                account.PhoneNumber = model.PhoneNumber;
                try
                {
                    _context.Users.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName}{account.LastName} Registered Successfully";
                }
                catch (DbUpdateException)
                {

                    ModelState.AddModelError("", "Please enter unique Model or Password ");
                    return View(model);
                }
                return View();

            }
            return View(model);

            static User GetAccount(RegistrationViewModel model)
            {
                return new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve user by email
                var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {

                        var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Email),
                    new("Name", user.FirstName),
                    new(ClaimTypes.Role, "User")
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError("", "Username/Email or Password is incorrect");
            }

            return View(model);
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SecurePage()
        {
            
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();

        }
    }
}
