using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
    }
}
