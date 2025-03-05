using Flappy_Clouds.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Controllers
{
    public class ProductController : Controller
    {
        private readonly FlappyCloudsContext _context;
        private const int DefaultPageSize = 6;

        public ProductController(FlappyCloudsContext context)
        {
            _context = context;
        }

        // ✅ Action to display paginated products
        [HttpGet]
        public async Task<IActionResult> Products(int page = 1, int pageSize = DefaultPageSize)
        {
            var totalProducts = await _context.Products.CountAsync();

            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var model = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }

        // ✅ Action to show single product details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var relatedProducts = await _context.Products
                .Where(p => p.Category == product.Category && p.ProductId != id && p.StockQuantity > 0)
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(viewModel);
        }

        // ✅ Suggestions for search bar (autocomplete)
        [HttpGet]
        public async Task<IActionResult> Suggest(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(term.ToLower()))
                .Select(p => new
                {
                    id = p.ProductId,
                    name = p.Name
                })
                .Take(5)
                .ToListAsync();

            return Json(products);
        }

        // ✅ Search results page
        [HttpGet]
        public async Task<IActionResult> SearchResults(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return View(new List<Product>());

            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(term.ToLower()))
                .Include(p => p.Category)
                .ToListAsync();

            return View(products);
        }
    }
}
