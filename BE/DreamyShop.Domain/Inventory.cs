using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Inventories")]
    public class Inventory : TrackEntity
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [InverseProperty(nameof(InventoryTicket.Inventory))]
        public virtual ICollection<InventoryTicket> InventoryTickets { get; set; }
    }
}