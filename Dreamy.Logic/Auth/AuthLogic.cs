using Dreamy.Common.Exceptions;
using Dreamy.Common.Results;
using Dreamy.Common.Utitlities;
using Dreamy.Domain.Shared.Dtos.Auth;
using Dreamy.Logic.Auth.Result;
using Dreamy.Logic.Auth.Security;
using Dreamy.Repository.Generic;

namespace Dreamy.Logic.Auth
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IRepositoryWrapper _repository;
        private readonly AccessToken _tokenService;

        public AuthLogic(
            IRepositoryWrapper repository,
            AccessToken tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public Task<ApiResult<bool>> ChangePassword(string email, UserChangePassword userChangePassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<AuthResult>> Login(LoginDto loginDto)
        {
            var email = loginDto.Email.ToLower();
            var phone = loginDto.Email.StandardPhone().GetLast9Digits();
            if (phone.Length < 9)
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.CredentialsInvalid);
            }
            var user = _repository.Auth.GetAll().Result.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var isPasswordMatched = Cryptography.VerifyPassword(loginDto.Password, user.StoredSalt, user.Password);
            if (isPasswordMatched)
            {
                var userResult = _repository.Auth.GetAll().Result.ToList().Where(u => u.Email.ToLower() == email || u.Phone.Contains(phone)).AsQueryable().GetAuthEntity();

                if (userResult == null)
                {
                    return new ApiErrorResult<AuthResult>((int)ErrorCodes.CredentialsInvalid);
                }

                var result = new AuthResult()
                {
                    Token = _tokenService.GenerateJwtToken(userResult),
                    IsAuthSuccessful = true,
                    User = userResult,
                };
                return new ApiSuccessResult<AuthResult>(result);
            }
            else
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.CredentialsInvalid);
            }
        }

        public Task<ApiResult<AuthResult>> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
