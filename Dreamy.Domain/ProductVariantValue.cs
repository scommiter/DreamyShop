namespace Dreamy.Domain
{
    public class ProductVariantValue : AuditEntity
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int ProductAttributeValueId { get; set; }
    }
}
