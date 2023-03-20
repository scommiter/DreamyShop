using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeTexts")]
    public class ProductAttributeText
    {
        public ProductAttributeText(Guid id, Guid attributeId, Guid productId, string value)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [InverseProperty("ProductAttributeTexts")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}