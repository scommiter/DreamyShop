using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;

namespace DreamyShop.Logic.User
{
    public interface IUserLogic
    {
        Task<ApiResult<PageResult<UserDto>>> GetAllUser(int page, int limit);
        Task<ApiResult<bool>> UpdateUser(string userId, UserUpdateDto userUpdateDto);
    }
}