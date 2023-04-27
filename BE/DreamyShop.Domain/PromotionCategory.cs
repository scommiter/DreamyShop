using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionCategories")]
    public class PromotionCategory : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionCategories")]
        public virtual Promotion Promotion { get; set; }
    }
}