using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Product;
using DreamyShop.Logic.Report;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IProductLogic _productService;
        private readonly IReportLogic _reportService;
        private readonly ILogger<ProductController> _logger;
        public ReportController(
            IProductLogic productService,
            IReportLogic reportService,
            ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = productService;
            _reportService = reportService;
        }
    }
}
