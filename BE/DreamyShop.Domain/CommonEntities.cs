using DreamyShop.Domain.Shared.Types;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamyShop.Domain
{
    public class AuditEntity
    {
        [Required]
        [DefaultValue((int)StatusType.Active)]
        public byte StatusID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }

    public class TrackEntity
    {
        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}