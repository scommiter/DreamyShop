using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Auth;
using DreamyShop.Logic.Conditions;
using DreamyShop.Logic.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _userService;
        public UserController(IUserLogic userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPut("user/update")]
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

        [Authorize]
        [HttpPut("user/searchCondition")]
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

        [Authorize]
        [HttpGet("user/getAll")]
        public async Task<IActionResult> GetAllUser([FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            var result = await _userService.GetAllUser(page, limit);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("user/delete")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }
    }
}
