using DreamyShop.Domain.Shared.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool GenderType { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
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
