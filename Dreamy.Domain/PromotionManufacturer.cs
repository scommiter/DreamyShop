namespace Dreamy.Domain
{
    public class PromotionManufacturer : AuditEntity
    {
        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public int PromotionId { get; set; }

    }
}