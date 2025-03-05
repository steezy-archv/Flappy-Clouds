using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flappy_Clouds.Entities;

namespace Flappy_Clouds.Controllers
{
    public class SearchController : Controller
    {
        private readonly FlappyCloudsContext _context;

        public SearchController(FlappyCloudsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Suggest(string term)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(term))
                .Select(p => new
                {
                    productId = p.ProductId,
                    name = p.Name
                })
                .Take(5)
                .ToListAsync();

            return Json(products);
        }
    }
}
