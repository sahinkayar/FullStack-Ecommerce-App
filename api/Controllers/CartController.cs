using api.Data;
using api.Dtos.Cart;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Cart")]
    public class CartController : ControllerBase
    {
       
        private readonly ICartRepository _cartRepo;

        public CartController( ICartRepository cartRepo)
        {
           
            _cartRepo = cartRepo;


        }
        [HttpGet]

        public async Task<IActionResult> GetCart()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();
            var cart = await _cartRepo.GetCartAsync(userId);

            if (cart == null) return Ok("No Cart Has been Found");
            return Ok(cart.ToCartDto());

        }

        [HttpPost("items")]

        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCardDto)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var cart = await _cartRepo.AddItemAsync(userId, addToCardDto.ProductId, addToCardDto.Quantity);
            if (cart == null) return NotFound("Couldn't find any product");

            return Ok(cart.ToCartDto());
        }

        [HttpPut("items/{productId}")]

        public async Task<IActionResult> UpdateCart([FromRoute] int productId, [FromBody] UpdateCartDto cartDto)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var cart = await _cartRepo.UpdateCartAsync(userId, productId, cartDto.quantity);
            if (cart == null) return NotFound("Product is not in cart");
            return Ok(cart.ToCartDto());


        }
        [HttpDelete("items/{productId}")]

        public async Task<IActionResult> DeleteCartItem([FromRoute] int productId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var cart = await _cartRepo.DeleteItemAsync(userId, productId);
            if (cart == null) return NotFound("Product is not in the cart");
            return Ok(cart.ToCartDto());

        }
    }
}
