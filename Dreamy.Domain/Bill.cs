using Dreamy.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("Bills")]
    public class Bill : AuditEntity
    {
        public int Id { get; set; }
        public string BillCode { get; set; }
        public int UserId { get; set; }
        public double TotalMoney { get; set; }
        public double ShippingFee { get; set; }
        public int Discount { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Phone { get; set; }
        public string? Note { get; set; }
        public string Address { get; set; }
        public string? ZipCode { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BillStatus BillStatus { get; set; }
        public DateTime? SuccessDate { get; set; }
        public DateTime? CancelDate { get; set; }
    }
}
