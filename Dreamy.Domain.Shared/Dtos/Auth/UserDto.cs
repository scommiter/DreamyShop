using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain.Shared.Dtos.Auth
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        public bool? GenderType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [StringLength(250)]
        public string? Avatar { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
        public List<byte> RoleTypes { get; set; }
    }

    public class UserChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
