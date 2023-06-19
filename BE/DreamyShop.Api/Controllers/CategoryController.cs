using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Category;
using DreamyShop.Logic.Category;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryLogic _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(
            ICategoryLogic categoryService,
            ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategory([FromQuery] PagingRequest pagingRequest)
        {
            var result = await _categoryService.GetAllCategory(pagingRequest);
            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize]
        [Member]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.CreateCategory(categoryCreateUpdateDto);
            return Ok(result);
        }

        [HttpPut("updateCategory")]
        [Authorize]
        [Member]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryCreateUpdateDto categoryCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.UpdateCategory(id, categoryCreateUpdateDto);
            return Ok(result);
        }

        [HttpDelete("removeCategory")]
        [Authorize]
        [Member]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.RemoveCategory(id);
            return Ok(result);
        }
    }
}
