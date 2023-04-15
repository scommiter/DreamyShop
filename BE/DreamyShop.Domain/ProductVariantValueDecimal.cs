using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValueDecimals")]
    public class ProductVariantValueDecimal : AuditEntity
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
        public Guid ProductAttributeDecimalId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValueDecimals")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValueDecimals")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValueDecimals")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeDecimalId))]
        [InverseProperty("ProductVariantValueDecimals")]
        public virtual ProductAttributeDecimal ProductAttributeDecimal { get; set; }
    }
}
