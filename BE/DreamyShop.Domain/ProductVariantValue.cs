using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("ProductVariantValues")]
    public class ProductVariantValue : AuditEntity
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

        public Guid ProductAttributeDateTimeId { get; set; }
        public Guid ProductAttributeDecimalId { get; set; }
        public Guid ProductAttributeIntId { get; set; }
        public Guid ProductAttributeTextId { get; set; }
        public Guid ProductAttributeVarcharId { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductVariantValues")]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(AttributeId))]
        [InverseProperty("ProductVariantValues")]
        public virtual Attribute Attribute { get; set; }

        [ForeignKey(nameof(ProductAttributeDateTimeId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeDateTime ProductAttributeDateTime { get; set; }

        [ForeignKey(nameof(ProductAttributeDecimalId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeDecimal ProductAttributeDecimal { get; set; }

        [ForeignKey(nameof(ProductAttributeIntId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeInt ProductAttributeInt{ get; set; }

        [ForeignKey(nameof(ProductAttributeTextId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeText ProductAttributeText { get; set; }

        [ForeignKey(nameof(ProductAttributeVarcharId))]
        [InverseProperty("ProductVariantValues")]
        public virtual ProductAttributeVarchar ProductAttributeVarchar { get; set; }
    }
}
