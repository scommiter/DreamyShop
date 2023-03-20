using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamyShop.Domain
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserID { get; set; }
        public byte RoleType { get; set; }

        [StringLength(2000)]
        public string ProfileUrl { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("Roles")]
        public virtual User User { get; set; }
    }
}