namespace api.models
{
    public class Cart
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
3    }
}
