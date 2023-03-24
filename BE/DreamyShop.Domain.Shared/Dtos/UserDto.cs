using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Dtos
{
    public class UserDto
    {
    }

    public class UserChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
