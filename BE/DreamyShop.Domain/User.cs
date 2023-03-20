using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }
        [StringLength(250)]
        public string Avatar { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string IdentityID { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(150)]
        public string Occupation { get; set; }
        [StringLength(150)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Password { get; set; }

        [InverseProperty(nameof(Role.User))]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
