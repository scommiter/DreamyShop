using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValueTexts")]
    public class ProductVariantValueText : AuditEntity
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
        public Guid ProductAttributeTextId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValueTexts")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValueTexts")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValueTexts")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeTextId))]
        [InverseProperty("ProductVariantValueTexts")]
        public virtual ProductAttributeText ProductAttributeText { get; set; }

    }
}
