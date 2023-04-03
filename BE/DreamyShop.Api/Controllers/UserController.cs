using DreamyShop.Api.Authorization;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Conditions;
using DreamyShop.Logic.User;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = DreamyShop.Api.Authorization.AuthorizeAttribute;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserLogic _userService;
        public UserController(IUserLogic userService)
        {
            _userService = userService;
        }

        [HttpPut("user/update")]
        [Admin]
        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = (AuthEntity)HttpContext.Items["Auth"];
            if (user == null)
            {
                return NotFound(user);
            }
            var result = await _userService.UpdateUser(user.UserID, userUpdateDto);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("user/searchCondition")]
        [Member]
        public async Task<IActionResult> SearchUser([FromForm] SearchUserCondition searchUserCondition)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Search(searchUserCondition);
            if (result.Result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("user/getAll")]
        [Admin]
        public async Task<IActionResult> GetAllUser([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var result = await _userService.GetAllUser(page, limit);
            return Ok(result);
        }

        [HttpDelete("user/delete")]
        [Admin]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }
    }
}
