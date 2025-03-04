using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Flappy_Clouds.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

[Route("Cart")]
public class CartController : Controller
{
    private readonly FlappyCloudsContext _context;

    public CartController(FlappyCloudsContext context)
    {
        _context = context;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return Unauthorized();
        }

        var cartItems = await _context.ShoppingCarts
            .Include(sc => sc.Product)
            .Where(sc => sc.UserId == userId)
            .ToListAsync();

        var totalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity);

        ViewBag.TotalAmount = totalAmount;

        return View(cartItems);
    }

    [HttpPost("AddToCart")]
    public async Task<IActionResult> AddToCart([FromBody] CartRequest request)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return Unauthorized();
        }


        var product = await _context.Products.FindAsync(request.ProductId);

        if (product == null || product.StockQuantity < request.Quantity)
        {
            return BadRequest(new { message = "Product not available." });
        }

        var existingCartItem = await _context.ShoppingCarts
            .FirstOrDefaultAsync(sc => sc.UserId == userId && sc.ProductId == request.ProductId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += request.Quantity;
        }
        else
        {
            var cartItem = new ShoppingCart
            {
                UserId = (int)userId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            await _context.ShoppingCarts.AddAsync(cartItem);
        }

        await _context.SaveChangesAsync();

        return Ok(new { message = "Product added to cart!" });
    }

    [HttpPost("UpdateQuantity")]
    public async Task<IActionResult> UpdateQuantity(int cartId, int change)
    {
        var cartItem = await _context.ShoppingCarts.FindAsync(cartId);

        if (cartItem != null)
        {
            cartItem.Quantity += change;
            if (cartItem.Quantity < 1)
            {
                _context.ShoppingCarts.Remove(cartItem);
            }
            else
            {
                _context.ShoppingCarts.Update(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        return Ok();
    }

    public class CartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
