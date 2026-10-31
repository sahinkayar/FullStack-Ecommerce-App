using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Product
{
    public class UpdateProductDto
    {
        [Required]
       public string Name { get; set; }=string.Empty;
        [Required]
        public string Description { get; set; }= string.Empty;
        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0")]

        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]

        public int Stock { get; set; }

        [Range(1,int.MaxValue)]
       public int CategoryId { get; set; }
    }
}
