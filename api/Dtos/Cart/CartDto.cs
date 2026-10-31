using api.models;

namespace api.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; } 
        public string UserId { get; set; } =string.Empty;



        public List<CartItemDto> CartItemDtos { get; set; } = new List<CartItemDto>();
        public decimal TotalPrice { get; set; }

    }
}
