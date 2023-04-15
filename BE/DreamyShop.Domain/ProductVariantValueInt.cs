using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValueInts")]
    public class ProductVariantValueInt : AuditEntity
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
        public Guid ProductAttributeIntId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValueInts")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValueInts")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValueInts")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeIntId))]
        [InverseProperty("ProductVariantValueInts")]
        public virtual ProductAttributeInt ProductAttributeInt { get; set; }
    }
}
