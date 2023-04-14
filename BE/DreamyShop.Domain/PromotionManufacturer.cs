using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionManufacturers")]
    public class PromotionManufacturer : AuditEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ManufactureId { get; set; }
        public Guid PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionManufacturers")]
        public virtual Promotion Promotion { get; set; }
    }
}