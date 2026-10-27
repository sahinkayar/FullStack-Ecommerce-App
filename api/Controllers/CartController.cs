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

            if (cart == null) return Ok("No order Has been Found");
            return Ok(cart.ToCartDto());

        }

        [HttpPost]

        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCardDto)
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);
            if (userName == null) return Unauthorized("Couldn't find user ");
            var user =await _usermanager.FindByNameAsync(userName);
            if (user == null) return Unauthorized();
            var product = await _context.Products.FindAsync(addToCardDto.ProductId);
            if (product == null) return NotFound("Couldn't find any product");
            var cart = await _context.Carts.Include((c) => c.CartItems).
                ThenInclude((ci) => ci.Product).
                FirstOrDefaultAsync((c) => c.AppUserId == user.Id);
            if (cart == null)
            {
                cart = new api.models.Cart { AppUserId = user.Id };
                _context.Carts.Add(cart);
                };
            await _context.SaveChangesAsync();
            return Ok(addToCardDto);
        }
    }
}
