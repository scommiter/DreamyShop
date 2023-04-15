using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariants")]
    public class ProductVariant : AuditEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }    
        public string SKU { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        [StringLength(250)]
        public string ThumbnailPicture { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValueDecimal.ProductVariant))]
        public virtual ICollection<ProductVariantValueDecimal> ProductVariantValueDecimals { get; set; }

        [InverseProperty(nameof(ProductVariantValueInt.ProductVariant))]
        public virtual ICollection<ProductVariantValueInt> ProductVariantValueInts { get; set; }

        [InverseProperty(nameof(ProductVariantValueVarchar.ProductVariant))]
        public virtual ICollection<ProductVariantValueVarchar> ProductVariantValueVarchars { get; set; }

        [InverseProperty(nameof(ProductVariantValueDateTime.ProductVariant))]
        public virtual ICollection<ProductVariantValueDateTime> ProductVariantValueDateTimes { get; set; }

        [InverseProperty(nameof(ProductVariantValueText.ProductVariant))]
        public virtual ICollection<ProductVariantValueText> ProductVariantValueTexts { get; set; }
    }
}