using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("Users")]
    public class User : AuditEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? IdentityID { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Country { get; set; }

        public string Password { get; set; }

        public byte[] StoredSalt { get; set; }
    }
}