using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
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
            return Ok();
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
            return Ok();
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
            return Ok();
        }

        [HttpDelete("removeCategory")]
        [Authorize]
        [Member]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            return Ok();
        }
    }
}
