using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeTexts")]
    public class ProductAttributeText
    {
        public ProductAttributeText() { }
        public ProductAttributeText(
            Guid id,
            Guid attributeId,
            Guid productId,
            string value,
            ProductAttribute productAttribute,
            Product product)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
            ProductAttribute = productAttribute;
            Product = product;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeTexts")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeTexts")]
        public virtual Product Product { get; set; }
    }
}