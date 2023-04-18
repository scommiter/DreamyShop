using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Promotions")]
    public class Promotion : AuditEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public bool RequireUseCouponCode { get; set; }
        public DateTime ValidDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public double DiscountAmount { get; set; }
        public bool LimitedUsageTimes { get; set; }
        public uint MaximumDiscountAmount { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty(nameof(PromotionCategory.Promotion))]
        public virtual ICollection<PromotionCategory> PromotionCategories { get; set; }

        [InverseProperty(nameof(PromotionManufacturer.Promotion))]
        public virtual ICollection<PromotionManufacturer> PromotionManufacturers { get; set; }

        [InverseProperty(nameof(PromotionProduct.Promotion))]
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}
