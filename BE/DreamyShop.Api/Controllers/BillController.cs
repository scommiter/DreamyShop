using DreamyShop.Domain.Shared.Dtos.Cart;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Bill;
using DreamyShop.Logic.Cart;
using Microsoft.AspNetCore.Mvc;
using DreamyShop.Domain.Shared.Dtos.Bill;
using DreamyShop.Logic.Conditions;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly IBillLogic _billService;
        private readonly ILogger<CategoryController> _logger;
        public BillController(
            IBillLogic billService,
            ILogger<CategoryController> logger)
        {
            _logger = logger;
            _billService = billService;
        }

        [HttpGet("getAllBill")]
        public async Task<IActionResult> GetAllBill(int userId)
        {
            var result = await _billService.GetBills(userId);
            return Ok(result.Result);
        }

        [HttpPost("createBill")]
        public async Task<IActionResult> CreateBill([FromForm] BillCreateDto billCreateDto)
        {
            var result = await _billService.CreateBill(billCreateDto);
            return Ok(result.Result);
        }

        [HttpPut("updateBill")]
        public async Task<IActionResult> UpdateBill([FromForm] BillUpdateDto billCreateDto, int userId, int billId)
        {
            var result = await _billService.UpdateBill(billCreateDto, userId, billId);
            return Ok(result.Result);
        }

        [HttpDelete("deleteBill")]
        public async Task<IActionResult> DeleteBill(int userId, int billId)
        {
            var result = await _billService.DeleteBill(userId, billId);
            return Ok(result.Result);
        }

        [HttpGet("searchBill")]
        public async Task<IActionResult> SearchBill(SearchBillCondition searchBillCondition)
        {
            var result = await _billService.SearchBill(searchBillCondition);
            return Ok(result.Result);
        }
    }
}
