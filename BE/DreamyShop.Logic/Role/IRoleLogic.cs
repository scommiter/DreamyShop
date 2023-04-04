using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Types;

namespace DreamyShop.Logic.Role
{
    public interface IRoleLogic
    {
        Task<ApiResult<bool>> AssignRole(Guid userId, List<byte> roleIds);
        Task<ApiResult<bool>> UpdateRole(Guid userId, List<byte> roleIds);
    }
}