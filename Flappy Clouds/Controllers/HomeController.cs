using System.Diagnostics;
using Flappy_Clouds.Entities;
using Flappy_Clouds.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly FlappyCloudsContext _context;

        public HomeController(FlappyCloudsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                isAdmin = user?.IsAdmin ?? false;
            }

            ViewBag.IsAdmin = isAdmin;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
