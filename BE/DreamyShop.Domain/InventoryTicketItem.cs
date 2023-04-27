using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("InventoryTicketItems")]
    public class InventoryTicketItem : TrackEntity
    {
        [Key]
        public int Id { get; set; }
        public int InventionTicketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiredDate { get; set; }

        [ForeignKey(nameof(InventionTicketId))]
        [InverseProperty("InventoryTicketItems")]
        public virtual InventoryTicket InventoryTicket { get; set; }
    }
}
