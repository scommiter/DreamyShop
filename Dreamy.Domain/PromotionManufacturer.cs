using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("PromotionManufacturers")]
    public class PromotionManufacturer : AuditEntity
    {
        public int Id { get; set; }
        public int ManufactureId { get; set; }
        public int PromotionId { get; set; }

    }
}