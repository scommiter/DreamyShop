using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Bill
{
    public interface IBillLogic
    {
        Task<ApiResult<bool>> CreateBill(BillCreateDto billCreateDto);
        Task<ApiResult<List<BillDto>>> GetBills(int userId);
    }
}
