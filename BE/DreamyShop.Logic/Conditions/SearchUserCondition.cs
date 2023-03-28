using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Conditions
{
    public class SearchUserCondition
    {
        public string? FullName { get; set; }
        public bool? GenderType { get; set; }

        public DateTime? Dob { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? IdentityID { get; set; }
        public List<byte>? RoleTypes { get; set; }
    }
}
