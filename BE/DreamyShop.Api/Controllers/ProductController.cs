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
        public async Task<IActionResult> GetAllProduct([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var result = await _productService.GetAllProduct(page, limit);
            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize]
        [Member]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateUpdateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateProduct(productCreateUpdateDto);
            return Ok(result);
        }

        [HttpPut("updateProduct")]
        [Authorize]
        [Member]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductCreateUpdateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProduct(id, productCreateUpdateDto);
            return Ok(result);
        }

        [HttpDelete("removeProduct")]
        //[Authorize]
        //[Member]
        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveProduct(id);
            return Ok(result);
        }

        [HttpGet("getListProductAttribute")]
        [Authorize]
        [Member]
        public async Task<IActionResult> GetListProductAttribute(Guid productId)
        {
            var result = await _productService.GetListProductAttribute(productId);
            return Ok(result);
        }

        [HttpPost("createAtributeProduct")]
        [Authorize]
        [Member]
        public async Task<IActionResult> CreateAtributeValueProduct([FromForm] CreateProductAttributeDto productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateAtributeValueProduct(productAttributeDto);
            return Ok(result);
        }

        [HttpPut("updateProductAttribute")]
        [Authorize]
        [Member]
        public async Task<IActionResult> UpdateProductAttributeAsync(Guid id, [FromForm] CreateProductAttributeDto productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProductAttributeAsync(id, productAttributeDto);
            return Ok(result);
        }

        [HttpDelete("removeAtributeProduct")]
        [Authorize]
        [Member]
        public async Task<IActionResult> RemoveAtributeProduct(Guid attributeId, Guid attributeTypeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveAtributeProduct(attributeId, attributeTypeId);
            return Ok(result);
        }


        [HttpPut("searchCondition")]
        public async Task<IActionResult> SearchProduct([FromForm] SearchProductCondition searchProductCondition)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.SearchProduct(searchProductCondition);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
