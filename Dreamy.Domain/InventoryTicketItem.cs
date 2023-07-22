using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("InventoryTicketItems")]
    public class InventoryTicketItem : TrackEntity
    {
        public int Id { get; set; }
        public int InventionTicketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
