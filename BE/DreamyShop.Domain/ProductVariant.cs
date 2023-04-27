using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("ProductVariants")]
    public class ProductVariant : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }    
        public string SKU { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [InverseProperty(nameof(ProductVariantValue.ProductVariant))]
        public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; }

        [InverseProperty(nameof(ProductVariantValue.ProductVariant))]
        public virtual ICollection<ImageProductVariant> ImageProductVariants { get; set; }


        [InverseProperty(nameof(BillDetail.ProductVariant))]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        [InverseProperty(nameof(CartDetail.ProductVariant))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}