using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Logic.Auth.Result;

namespace DreamyShop.Logic.Auth
{
    public interface IAuthLogic
    {
        Task<ApiResult<AuthResult>> Register(RegisterDto registerDto);
        Task<ApiResult<AuthResult>> Login(LoginDto loginDto);
    }
}