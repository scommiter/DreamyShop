using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Manufacturer;
using DreamyShop.Logic.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = DreamyShop.Api.Authorization.AuthorizeAttribute;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerLogic _manufacturerService;
        private readonly ILogger<ManufacturerController> _logger;
        public ManufacturerController(
            IManufacturerLogic manufacturerService, 
            ILogger<ManufacturerController> logger)
        {
            _logger = logger;
            _manufacturerService = manufacturerService;
        }

        [HttpGet("manufacturer/getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllManufacturer([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var result = await _manufacturerService.GetAllManufacturer(page, limit);
            return Ok(result);
        }

        [HttpPost("manufacturer/create")]
        [Member]
        public async Task<IActionResult> CreateManufacturer([FromForm] ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.CreateManufacturer(manufacturerCreateUpdateDto);
            return Ok(result);
        }

        [HttpPut("manufacturer/updateManufacturer")]
        [Member]
        public async Task<IActionResult> UpdateManufacturer(Guid id, [FromForm] ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.UpdateManufacturer(id, manufacturerCreateUpdateDto);
            return Ok(result);
        }

        [HttpDelete("manufacturer/removeManufacturer")]
        [Member]
        public async Task<IActionResult> RemoveManufacturer(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.RemoveManufacturer(id);
            return Ok(result);
        }
    }
}
