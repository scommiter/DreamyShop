using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class RoleDto
    {
        public int UserID { get; set; }
        public byte RoleType { get; set; }
        public string? ProfileUrl { get; set; }
    }
}
