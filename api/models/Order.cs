using System.ComponentModel.DataAnnotations.Schema;

namespace api.models
{
    [Table("Orders")]
    public class Order
    {

        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
