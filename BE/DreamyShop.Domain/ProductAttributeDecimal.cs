using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDecimals")]
    public class ProductAttributeDecimal : TrackEntity
    {
        public ProductAttributeDecimal() { }
        public ProductAttributeDecimal(
            Guid id,
            Guid attributeId,
            Guid productId,
            decimal value,
            Attribute attribute,
            Product product)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
            Attribute = attribute;
            Product = product;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeDecimals")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeDecimals")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValueDecimal.ProductAttributeDecimal))]
        public virtual ICollection<ProductVariantValueDecimal> ProductVariantValueDecimals { get; set; }
    }
}