using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Flappy_Clouds.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Flappy_Clouds.Controllers
{
    public class CartController : Controller
    {
        private readonly FlappyCloudsContext _context;

        public CartController(FlappyCloudsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var cartItems = await _context.ShoppingCarts
                .Where(c => userId == null || c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            return View(cartItems);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = HttpContext.Session.GetInt32("UserId"); 

            if (userId == null)
            {
                Console.WriteLine("Guest user detected, setting UserId to NULL");
            }
            else
            {
                Console.WriteLine($"Logged in UserId: {userId}");
            }

            var cartItem = new ShoppingCart
            {
                UserId = userId,  
                ProductId = productId,
                Quantity = quantity
            };

            _context.ShoppingCarts.Add(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> GuestCheckout(string CustomerName, string CustomerPhone, string CustomerAddress)
        {
            var cartItems = await _context.ShoppingCarts.Include(c => c.Product).ToListAsync();
            if (!cartItems.Any()) return RedirectToAction("Index");

            var order = new Order
            {
                UserId = null,
                CustomerName = CustomerName,
                CustomerPhone = CustomerPhone,
                CustomerAddress = CustomerAddress,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                OrderStatus = "Pending",
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();
            _context.ShoppingCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.ShoppingCarts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
