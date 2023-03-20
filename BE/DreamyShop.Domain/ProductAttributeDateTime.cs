using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDateTimes")]
    public class ProductAttributeDateTime
    {
        public ProductAttributeDateTime(Guid id, Guid attributeId, Guid productId, DateTime? value)
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
        public DateTime? Value { get; set; }

        [InverseProperty("ProductAttributeDateTimes")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}