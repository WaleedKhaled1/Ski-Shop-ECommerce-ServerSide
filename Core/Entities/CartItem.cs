namespace Core.Entities
{
    public class CartItem
    {
        public int productId { get; set; }
        public required string productName { get; set; }
        public decimal price { get; set; }
        public required string pictureUrl { get; set; }
        public required string type { get; set; }
        public required string brand { get; set; }
        public int quantity { get; set; }
    }
}