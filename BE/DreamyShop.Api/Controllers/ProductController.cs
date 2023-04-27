using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Conditions;
using DreamyShop.Logic.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = DreamyShop.Api.Authorization.AuthorizeAttribute;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductLogic _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(
            IProductLogic productService,
            ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProduct([FromQuery] PagingRequest pagingRequest)
        {
            return Ok();
        }

        [HttpPost("create")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPut("updateProduct")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpDelete("removeProduct")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPut("searchCondition")]
        public async Task<IActionResult> SearchProduct([FromForm] SearchProductCondition searchProductCondition, [FromQuery] PagingRequest pagingRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }

        [HttpPost("uploadImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(int productId, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost("uploadMultipleImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMultipleFile(int productId, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
