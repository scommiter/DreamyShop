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

        [InverseProperty(nameof(ProductVariantValue.ProductVariant))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }
    }
}