using api.Data;
using api.Dtos.Cart;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Cart")]
    public class CartController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly UserManager<AppUser> _usermanager;

        public CartController(ECommerceDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }
        [HttpGet]

        public async Task<IActionResult> GetCart()
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized("Couldn't find User ");
            var user = await _usermanager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.AppUserId == user.Id);

            if (cart == null) return Ok("No Cart Has been Found");
            return Ok(cart.ToCartDto());

        }

        [HttpPost]

        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCardDto)
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized("Couldn't find user");
            var user = await _usermanager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
            var product = await _context.Products.FindAsync(addToCardDto.ProductId);
            if (product == null) return NotFound("Couldn't find any product");
            var cart = await _context.Carts.Include((c) => c.CartItems).
                ThenInclude((ci) => ci.Product).
                FirstOrDefaultAsync((c) => c.AppUserId == user.Id);
            if (cart == null)
            {
                cart = new Cart { AppUserId = user.Id };
                _context.Carts.Add(cart);
            }
            ;

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addToCardDto.ProductId);

            if (item == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = addToCardDto.ProductId,
                    Quantity = addToCardDto.Quantity,
                    Product = product
                });
            }
            else
            {
                item.Quantity += addToCardDto.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok(addToCardDto);
        }

        [HttpPut("{productId}")]

        public async Task<IActionResult> UpdateCart([FromRoute] int productId, [FromBody] UpdateCartDto updateDto)
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized();
            var user = await _usermanager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();

            var cart =await _context.Carts.Include((c) => c.CartItems).ThenInclude((p) => p.Product).FirstOrDefaultAsync((v) => v.AppUserId == user.Id);

            if (cart == null) return NotFound("Cart not found");

            var item = cart.CartItems.FirstOrDefault((c)=> c.ProductId == productId);
            if (item == null) return NotFound("Please check Again");
            item.Quantity = updateDto.quantity;

            await _context.SaveChangesAsync();
            return Ok(cart.ToCartDto());


        }
        [HttpDelete("{productId}")]

        public async Task<IActionResult> DeleteCart([FromRoute] int productId)
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized();
            var user=await _usermanager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
            var cart = await _context.Carts.Include((c) => c.CartItems).ThenInclude((p) => p.Product).FirstOrDefaultAsync((v) => v.AppUserId == user.Id);
            if (cart == null) return NotFound("Cart not Found");
            var item = cart.CartItems.FirstOrDefault((c) => c.ProductId == productId);
            if (item == null) return NotFound();
               _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
 
        }
    }
}
