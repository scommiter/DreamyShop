using DreamyShop.Domain.Shared.Dtos.Product;
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

        [HttpGet("excell/Export")]
        public async Task<IActionResult> DownloadReport()
        {
            string reportname = $"ProductReport-{DateTime.Now.ToString("MM/dd/yyyy")}.xlsx";
            var products = await _productService.GetAllProduct();
            if (products.Result.Items.Count == 0)
            {
                return BadRequest(products);
            }
            var exportfile = _reportService.ExporttoExcel<ProductDto>(products.Result.Items, reportname);
            return File(exportfile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
        }

        [HttpPost("excell/Import")]
        public async Task<IActionResult> ReadExcel(IFormFile reportFile)
        {
            var exportfile = await _reportService.ReadFromExcel(reportFile);
            if (exportfile.Result == null)
            {
                return BadRequest(exportfile);
            }
            await _productService.ImportProducts(exportfile.Result);
            //foreach (var productCreateDto in exportfile.Result)
            //{
            //    await _productService.CreateProduct(productCreateDto);
            //}
            return Ok();
        }
    }
}
