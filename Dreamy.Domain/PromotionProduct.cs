namespace Dreamy.Domain
{
    public class PromotionProduct : AuditEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int PromotionId { get; set; }
    }
}