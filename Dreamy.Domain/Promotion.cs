using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("Promotions")]
    public class Promotion : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public bool RequireUseCouponCode { get; set; }
        public DateTime ValidDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public double DiscountAmount { get; set; }
        public bool LimitedUsageTimes { get; set; }
        public uint MaximumDiscountAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
