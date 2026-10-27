using api.Dtos.Cart;
using api.models;

namespace api.Mappers
{
    public static class CartMapper
    {

        public static CartDto ToCartDto(this Cart cart )
        {

            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.AppUserId,
                CartItemDtos = cart.CartItems.Select((c) => new CartItemDto
                {
                    ProductId = c.ProductId,
                    Price = c.Product.Price,
                    LineTotal = c.Quantity * c.Product.Price,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity

                }).ToList()
            };
        }  
    }
}
