using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Bills")]
    public class Bill : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public double TotalMoney { get; set; }
        [Required]
        public double ShippingFee { get; set; }
        public int Discount { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }     
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        public string? Note { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        public string? ZipCode { get; set; }
        [Required]
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        public BillStatus BillStatus { get; set; }
        public DateTime? SuccessDate { get; set; }
        public DateTime? CancelDate { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bills")]
        public virtual User User { get; set; }

        [InverseProperty(nameof(BillDetail.Bill))]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
