namespace Dreamy.Domain
{
    public class PromotionCategory : AuditEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PromotionId { get; set; }
    }
}