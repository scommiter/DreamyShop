using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeTexts")]
    public class ProductAttributeText : TrackEntity
    {
        public ProductAttributeText() { }
        public ProductAttributeText(
            Guid id,
            Guid attributeId,
            Guid productId,
            string value,
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
        public string Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeTexts")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeTexts")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValue.ProductAttributeText))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }
    }
}