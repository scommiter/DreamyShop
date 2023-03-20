using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDecimals")]
    public class ProductAttributeDecimal
    {
        public ProductAttributeDecimal(Guid id, Guid attributeId, Guid productId, decimal value)
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
        public decimal Value { get; set; }

        [InverseProperty("ProductAttributeDecimals")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}