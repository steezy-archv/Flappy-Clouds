using Flappy_Clouds.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Controllers
{
    public class ProductController : Controller
    {
        private readonly FlappyCloudsContext _context;
        private const int DefaultPageSize = 12;

        public ProductController(FlappyCloudsContext context)
        {
            _context = context;
        }

        // Action to display all products
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(products);
        }

        // Action to show single product details
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

        //public async Task<IActionResult> Index(int page = 1, int pageSize = DefaultPageSize)
        //{
        //    var totalProducts = await _context.Products.CountAsync();

        //    var products = await _context.Products
        //        .OrderBy(p => p.ProductId)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

        //    var model = new ProductListViewModel
        //    {
        //        Products = products,
        //        CurrentPage = page,
        //        TotalPages = totalPages
        //    };

        //    return View(model);
        //}


    }
}
