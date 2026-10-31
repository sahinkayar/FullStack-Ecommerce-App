using api.Data;
using api.Mappers;
using api.Migrations;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/Order")]
    public class OrderController:ControllerBase
    {

        private readonly ECommerceDbContext _context;
        private readonly UserManager<AppUser> _userManager ;
        public OrderController(ECommerceDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager= userManager;
        }
        [HttpGet]

        public async Task<IActionResult> GetOrder()
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
            var orders = await _context.Orders.Include((o)=>o.OrderItems).ThenInclude((p)=>p.Product).FirstOrDefaultAsync((u)=>u.AppUserId==user.Id);
            if(orders == null) return NotFound("No Order Has been found");
            return Ok(orders.toDtoFromDb());


        }
        [HttpPost]

        public async Task<IActionResult> CreateOrder()
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
           var cart =  await _context.Carts.Include((o) => o.CartItems).ThenInclude((p) => p.Product).FirstOrDefaultAsync((u) => u.AppUserId == user.Id);
            if (cart == null || cart.CartItems.Count == 0) return BadRequest("Cart Is Empty");
            foreach (var item in cart.CartItems)
            {
                if (item.Product?.Stock <item.Quantity)
                {
                    return BadRequest($"Not enough stock for {item.Product.Name}");

                }
               
            }
            var order = new Order { AppUserId = user.Id };

            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Price = item.Product!.Price,
                    
                    

                });
                item.Product.Stock -= item.Quantity;
            }
            order.TotalPrice = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return Ok(order.toDtoFromDb());


        }

    }
}
