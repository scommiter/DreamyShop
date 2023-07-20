using Dreamy.Common.Results;
using Dreamy.Domain.Shared.Dtos.Auth;
using Dreamy.Logic.Auth.Result;

namespace Dreamy.Logic.Auth
{
    public interface IAuthLogic
    {
        Task<ApiResult<bool>> Register(RegisterDto registerDto);
        Task<ApiResult<AuthResult>> Login(LoginDto loginDto);
        Task<ApiResult<bool>> ChangePassword(string email, UserChangePassword userChangePassword);
    }
}
