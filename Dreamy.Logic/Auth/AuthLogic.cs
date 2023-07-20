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
                var userResult = _repository.Auth.GetAll().Result.ToList().Where(u => u.Email.ToLower() == email || u.Phone.Contains(phone)).AsQueryable().GetAuthEntity(_repository.Role.GetAll().Result.ToList());

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

        public async Task<ApiResult<AuthResult>> Register(RegisterDto registerDto)
        {
            //if (_repository.Auth.GetAll().Result.AsQueryable().IsEmailExist(registerDto.Email) || _repository.Auth.GetAll().Result.AsQueryable().IsPhoneExist(registerDto.Phone))
            //{
            //    return new ApiErrorResult<AuthResult>((int)ErrorCodes.DuplicatedData);
            //}
            var test = _repository.Auth.GetAll();

            var user = new Domain.User()
            {
                FullName = registerDto.FullName,
                GenderType = registerDto.GenderType == "male" ? false : true,
                Phone = registerDto.Phone,
                Dob = registerDto.Dob,
                Email = registerDto.Email,
                DateCreated = DateTime.Now,
            };

            var hashsalt = Cryptography.EncryptPassword(registerDto.Password);
            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                user.Password = hashsalt.Hash;
                user.StoredSalt = hashsalt.Salt;
            }
            await _repository.Auth.Register(user);
            var userResult = _repository.Auth.GetAll().Result.AsQueryable().GetAuthEntity(_repository.Role.GetAll().Result.ToList());
            var result = new AuthResult()
            {
                Token = _tokenService.GenerateJwtToken(userResult),
                IsAuthSuccessful = true,
                User = userResult,
            };
            return new ApiSuccessResult<AuthResult>(result);
        }
    }
}
