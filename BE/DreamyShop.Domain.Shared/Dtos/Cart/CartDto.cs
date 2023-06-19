namespace DreamyShop.Domain.Shared.Dtos.Cart
{
    public class CartItemsDto
    {
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
    }

    public class CartAddDto
    {
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
