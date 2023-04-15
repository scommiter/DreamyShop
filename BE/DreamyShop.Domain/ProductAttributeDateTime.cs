using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductAttributeDateTimes")]
    public class ProductAttributeDateTime : TrackEntity
    {
        public ProductAttributeDateTime() { }

        [Key]
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime? Value { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductAttributeDateTimes")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductAttributeDateTimes")]
        public virtual Product Product{ get; set; }

        [InverseProperty(nameof(ProductVariantValueDateTime.ProductAttributeDateTime))]
        public virtual ICollection<ProductVariantValueDateTime> ProductVariantValueDateTimes { get; set; }
    }
}