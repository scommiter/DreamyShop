using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDateTimes")]
    public class ProductAttributeDateTime
    {
        public ProductAttributeDateTime() { }
        public ProductAttributeDateTime(
            Guid id, 
            Guid attributeId, 
            Guid productId, 
            DateTime? value, 
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
        public DateTime? Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeDateTimes")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeDateTimes")]
        public virtual Product Product{ get; set; }
    }
}