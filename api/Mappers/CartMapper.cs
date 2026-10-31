using api.Dtos.Cart;
using api.models;

namespace api.Mappers
{
    public static class CartMapper
    {

        public static CartDto ToCartDto(this Cart cart )
        {
            var items = cart.CartItems.Select(c => new CartItemDto
            {
                ProductId = c.ProductId,
                ProductName = c.Product!.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity,
                LineTotal = c.Quantity * c.Product.Price
            }).ToList();

            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.AppUserId,
                CartItemDtos = items,
                 TotalPrice= items.Sum(i => i.LineTotal)
            };
        }  
    }
}
