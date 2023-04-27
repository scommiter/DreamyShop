using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValues")]
    public class ProductVariantValue : AuditEntity
    {
        [Key]
        [Column(Order = 0)]
        public int ProductVariantId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int AttributeId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int ProductAttributeValueId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValues")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValues")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeValueId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeValue ProductAttributeValues { get; set; }
    }
}
