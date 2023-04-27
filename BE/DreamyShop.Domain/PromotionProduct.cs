using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionProducts")]
    public class PromotionProduct : AuditEntity
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionProducts")]
        public virtual Promotion Promotion { get; set; }
    }
}