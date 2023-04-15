using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValueDateTimes")]
    public class ProductVariantValueDateTime : AuditEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid ProductVariantId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid AttributeId { get; set; }
        [Key]
        [Column(Order = 3)]
        public Guid ProductAttributeDateTimeId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValueDateTimes")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValueDateTimes")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValueDateTimes")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeDateTimeId))]
        [InverseProperty("ProductVariantValueDateTimes")]
        public virtual ProductAttributeDateTime ProductAttributeDateTime { get; set; }
    }
}
