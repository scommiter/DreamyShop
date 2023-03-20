using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("InventoryTicketItems")]
    public class InventoryTicketItem
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
