using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("InventoryTickets")]
    public class InventoryTicket : AuditEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int InventoryId { get; set; }
        public bool IsApproved { get; set; }
        public int? ApproverId { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}