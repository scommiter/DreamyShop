using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionCategories")]
    public class PromotionCategory : AuditEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionCategories")]
        public virtual Promotion Promotion { get; set; }
    }
}