using api.Dtos.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
