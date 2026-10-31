using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Cart
{
    public class UpdateCartDto
    {
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int quantity { get; set; }
    }
}
