using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthLogic _authService;
        public AuthController(IAuthLogic authenService)
        {
            _authService = authenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.Register(userRegisterDto);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.Login(userLoginDto);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] UserChangePassword userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = (AuthEntity)HttpContext.Items["Auth"];
            var result = await _authService.ChangePassword(user.Email, userLoginDto);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
