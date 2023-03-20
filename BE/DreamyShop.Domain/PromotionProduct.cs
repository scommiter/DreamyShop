using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("PromotionProducts")]
    public class PromotionProduct
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }

        [ForeignKey(nameof(PromotionId))]
        [InverseProperty("PromotionProducts")]
        public virtual Promotion Promotion { get; set; }
    }
}