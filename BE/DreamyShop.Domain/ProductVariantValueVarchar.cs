using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValueVarchars")]
    public class ProductVariantValueVarchar : AuditEntity
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
        public Guid ProductAttributeVarcharId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValueVarchars")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValueVarchars")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValueVarchars")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeVarcharId))]
        [InverseProperty("ProductVariantValueVarchars")]
        public virtual ProductAttributeVarchar ProductAttributeVarchar { get; set; }
    }
}
