﻿using DreamyShop.Logic.Chart;
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
    }
}