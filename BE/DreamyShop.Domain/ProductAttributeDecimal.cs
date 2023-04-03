using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDecimals")]
    public class ProductAttributeDecimal
    {
        public ProductAttributeDecimal() { }
        public ProductAttributeDecimal(
            Guid id, 
            Guid attributeId, 
            Guid productId, 
            decimal value,
            ProductAttribute productAttribute)
        {
            Id = id;
            AttributeId = attributeId;
            ProductId = productId;
            Value = value;
            ProductAttribute = productAttribute;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Value { get; set; }

        [InverseProperty("ProductAttributeDecimals")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}