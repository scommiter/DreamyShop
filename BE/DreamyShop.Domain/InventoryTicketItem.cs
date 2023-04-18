using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("InventoryTicketItems")]
    public class InventoryTicketItem : TrackEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid InventionTicketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiredDate { get; set; }

        [ForeignKey(nameof(InventionTicketId))]
        [InverseProperty("InventoryTicketItems")]
        public virtual InventoryTicket InventoryTicket { get; set; }
    }
}
