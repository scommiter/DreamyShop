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
    public class UserController : Controller
    {
        private readonly IUserLogic _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(
            IUserLogic userService, 
            ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPut("update")]
        [Authorize]
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

        [HttpPut("searchCondition")]
        [Authorize]
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

        [HttpGet("getAll")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> GetAllUser(PagingRequest pagingRequest)
        {
            var result = await _userService.GetAllUser(pagingRequest);
            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }
    }
}
