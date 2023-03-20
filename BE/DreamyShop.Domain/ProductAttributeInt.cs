using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeInts")]
    public class ProductAttributeInt
    {
        public ProductAttributeInt(Guid id, Guid attributeId, Guid productId, int value)
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
        public int Value { get; set; }

        [InverseProperty("ProductAttributeInts")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}