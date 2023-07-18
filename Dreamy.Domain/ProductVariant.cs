namespace Dreamy.Domain
{
    public class ProductVariant : AuditEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ThumbnailPicture { get; set; }
    }
}