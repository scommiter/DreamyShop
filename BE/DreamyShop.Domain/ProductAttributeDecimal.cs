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
        public decimal Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeDecimals")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeDecimals")]
        public virtual Product Product { get; set; }
    }
}