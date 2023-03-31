using DreamyShop.Logic.Product;
using DreamyShop.Logic.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductLogic _productService;
        public ProductController(IProductLogic productService)
        {
            _productService = productService;
        }

        [HttpGet("product/getAll")]
        public async Task<IActionResult> GetAllProduct([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var result = await _productService.GetAllProduct(page, limit);
            return Ok(result);
        }

    }
}
