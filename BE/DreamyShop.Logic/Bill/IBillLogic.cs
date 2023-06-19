using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos.Bill;
using DreamyShop.Logic.Conditions;
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
        Task<ApiResult<List<BillDto>>> SearchBill(SearchBillCondition searchBillCondition);
        Task<ApiResult<bool>> UpdateBill(BillUpdateDto billCreateDto, int userId, int billId);
        Task<ApiResult<bool>> DeleteBill(int userId, int billId);
    }
}
