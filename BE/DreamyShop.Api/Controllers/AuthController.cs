using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = DreamyShop.Api.Authorization.AuthorizeAttribute;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthLogic _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            IAuthLogic authenService, 
            ILogger<AuthController> logger)
        {
            _logger = logger;
            _authService = authenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userRegisterDto)
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userLoginDto)
        {
            return Ok();
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        [Member]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePassword userLoginDto)
        {
            return Ok();
        }
    }
}
