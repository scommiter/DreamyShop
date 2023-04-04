using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Product;
using DreamyShop.Logic.Role;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleLogic _roleLogic;
        private readonly ILogger<RoleController> _logger;
        public RoleController(
            ILogger<RoleController> logger, 
            IRoleLogic roleLogic)
        {
            _logger = logger;
            _roleLogic = roleLogic;
        }

        [HttpPost("assignRole")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> AssignRole(Guid userId, [FromForm] List<byte> roleIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleLogic.AssignRole(userId, roleIds);
            return Ok(result);
        }

        [HttpPost("updateRole")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> UpdateRole(Guid userId, [FromForm] List<byte> roleIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleLogic.UpdateRole(userId, roleIds);
            return Ok(result);
        }
    }
}
