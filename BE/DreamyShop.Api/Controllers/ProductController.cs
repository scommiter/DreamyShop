using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Product;
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

        [HttpPost("product/create")]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateUpdateDto productCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateProduct(productCreateUpdateDto);
            return Ok(result);
        }

        [HttpGet("product/getListProductAttribute")]
        [Authorize]
        public async Task<IActionResult> GetListProductAttribute(Guid productId)
        {
            var result = await _productService.GetListProductAttribute(productId);
            return Ok(result);
        }

        [HttpPost("product/createAtributeProduct")]
        [Authorize]
        public async Task<IActionResult> CreateAtributeProduct([FromForm] CreateProductAttributeDto productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateAtributeProduct(productAttributeDto);
            return Ok(result);
        }

        [HttpPut("product/updateProductAttribute")]
        [Authorize]
        public async Task<IActionResult> UpdateProductAttributeAsync(Guid id, [FromForm] CreateProductAttributeDto productAttributeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProductAttributeAsync(id, productAttributeDto);
            return Ok(result);
        }

        [HttpDelete("product/removeAtributeProduct")]
        [Authorize]
        public async Task<IActionResult> RemoveAtributeProduct(Guid attributeId, Guid attributeTypeId)
        {
            var result = await _productService.RemoveAtributeProduct(attributeId, attributeTypeId);
            return Ok(result);
        }
    }
}
