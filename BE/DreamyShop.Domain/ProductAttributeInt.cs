using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeInts")]
    public class ProductAttributeInt : TrackEntity
    {
        public ProductAttributeInt() { }
        public ProductAttributeInt(
            Guid id,
            Guid attributeId,
            Guid productId,
            int value,
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
        public int Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeInts")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeInts")]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValueInt.ProductAttributeInt))]
        public virtual ICollection<ProductVariantValueInt> ProductVariantValueInts { get; set; }
    }
}