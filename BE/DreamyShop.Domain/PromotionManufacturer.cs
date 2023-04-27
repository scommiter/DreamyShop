using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionManufacturers")]
    public class PromotionManufacturer : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionManufacturers")]
        public virtual Promotion Promotion { get; set; }
    }
}