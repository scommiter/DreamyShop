using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Logic.Role
{
    public interface IRoleLogic
    {
        Task<ApiResult<bool>> AssignRole(int userId, List<byte> roleIds);
        Task<ApiResult<bool>> UpdateRole(int userId, List<byte> roleIds);
    }
}