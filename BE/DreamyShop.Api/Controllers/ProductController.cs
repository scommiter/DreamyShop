using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Product;
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
            var result = await _productService.GetAllProductPaging(pagingRequest);
            return Ok(result.Result);
        }

        [HttpPost("create"), DisableRequestSizeLimit]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateProduct(productCreateUpdateDto);
            return Ok(result.Result);
        }

        [HttpPut("{id}")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProduct(id, productCreateUpdateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveProduct(id);
            return Ok(result);
        }

        [HttpPut("searchCondition")]
        public async Task<IActionResult> SearchProduct([FromForm] SearchProductCondition searchProductCondition, [FromQuery] PagingRequest pagingRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.SearchProduct(searchProductCondition, pagingRequest);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("uploadMultipleImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMultipleFile(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files.ToList();
            var result = await _productService.UploadMultipleImage(files, productId);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
