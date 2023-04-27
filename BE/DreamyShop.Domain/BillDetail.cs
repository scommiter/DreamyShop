using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("BillDetails")]
    public class BillDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid VariantProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double Tax { get; set; }
        public string Note { get; set; }

        [ForeignKey(nameof(BillId))]
        [InverseProperty("BillDetails")]
        public virtual Bill Bill { get; set; }

        [ForeignKey(nameof(VariantProductId))]
        [InverseProperty("BillDetails")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
