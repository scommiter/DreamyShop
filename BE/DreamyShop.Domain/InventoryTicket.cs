using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("InventoryTickets")]
    public class InventoryTicket : AuditEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public int InventoryId { get; set; }
        public bool IsApproved { get; set; }
        public int? ApproverId { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [ForeignKey(nameof(InventoryId))]

        [InverseProperty("InventoryTickets")]
        public virtual Inventory Inventory { get; set; }

        [InverseProperty(nameof(InventoryTicketItem.InventoryTicket))]
        public virtual ICollection<InventoryTicketItem> InventoryTicketItems { get; set; }
    }
}