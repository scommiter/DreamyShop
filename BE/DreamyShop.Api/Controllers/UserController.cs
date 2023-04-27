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
            return Ok();
        }

        [HttpPut("searchCondition")]
        [Authorize]
        [Member]
        public async Task<IActionResult> SearchUser([FromForm] SearchUserCondition searchUserCondition)
        {
            return Ok();
        }

        [HttpGet("getAll")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> GetAllUser(PagingRequest pagingRequest)
        {
            return Ok();
        }

        [HttpDelete("delete")]
        [Authorize]
        [Admin]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok();
        }
    }
}
