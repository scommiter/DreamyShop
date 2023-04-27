using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.Types
{
    public enum PaymentStatus
    {
        Unpaid = 1,
        Failed = 2,
        Expired = 3,
        Paid = 4,
        Refunding = 5,
        Refunded = 6,
    }
}
