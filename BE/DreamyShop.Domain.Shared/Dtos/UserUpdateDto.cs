using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? IdentityID { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Country { get; set; }
        public string? ProfileUrl { get; set; }
    }
}
