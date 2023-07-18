using Dreamy.Domain.Shared.Types;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dreamy.Domain
{
    public class AuditEntity
    {
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