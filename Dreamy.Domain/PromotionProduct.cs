using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("PromotionProducts")]
    public class PromotionProduct : AuditEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int PromotionId { get; set; }
    }
}