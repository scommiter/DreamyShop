using DreamyShop.Domain.Shared.Dtos.Chart;
using DreamyShop.Logic.Chart;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly IChartLogic _chartService;
        private readonly ILogger<CategoryController> _logger;
        public ChartController(
            IChartLogic chartService,
            ILogger<CategoryController> logger)
        {
            _logger = logger;
            _chartService = chartService;
        }

        [HttpGet("getChartSalesWeekly")]
        public async Task<IActionResult> GetChartSalesWeekly()
        {
            var result = await _chartService.GetChartWeeklySale();
            return Ok(result.Result);
        }

        [HttpGet("getChartMonthlySale")]
        public async Task<IActionResult> GetChartMonthlySale()
        {
            var result = await _chartService.GetChartMonthlySale();
            return Ok(result.Result);
        }

        [HttpGet("getStatisticDashboard")]
        public async Task<IActionResult> GetStatisticDashboard()
        {
            var result = await _chartService.GetStatisticDashboard();
            return Ok(result.Result);
        }

        [HttpGet("getPricePaymentType")]
        public async Task<IActionResult> GetPricePaymentType()
        {
            var result = await _chartService.GetPricePaymentType();
            return Ok(result.Result);
        }

        [HttpGet("getChartInYearSale")]
        public async Task<IActionResult> GetChartInYearSale()
        {
            var result = await _chartService.GetChartInYearSale();
            return Ok(result.Result);
        }
    }
}
