using api.models;

namespace api.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; } 
        public string UserId { get; set; } =string.Empty;
        public AppUser? AppUser { get; set; }


        public List<CartItemDto> CartItemDtos = new List<CartItemDto>();

    }
}
