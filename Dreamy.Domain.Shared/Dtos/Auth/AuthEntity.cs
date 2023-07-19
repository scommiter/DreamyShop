using System.ComponentModel.DataAnnotations;

namespace Dreamy.Domain.Shared.Dtos.Auth
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string GenderType { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
    }

    public class AuthEntity
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public List<byte> RoleTypes { get; set; }
    }
}
