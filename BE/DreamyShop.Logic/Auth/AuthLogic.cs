using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Result;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Repository.Helpers;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DreamyShop.Logic.Auth
{
    public class AuthLogic : IAuthLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly AccessToken _tokenService;
        private readonly IMapper _mapper;

        public AuthLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            AccessToken tokenService,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ApiResult<AuthResult>> Login(LoginDto loginDto)
        {
            var email = loginDto.Email.ToLower();
            var phone = loginDto.Email.StandardPhone().GetLast9Digits();
            if (phone.Length < 9)
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.CredentialsInvalid);
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var isPasswordMatched = Cryptography.VerifyPassword(loginDto.Password, user.StoredSalt, user.Password);
            if (isPasswordMatched)
            {
                var userResult = _context.Users
               .Include(u => u.Roles)
               .AsNoTracking()
               .Where(u => u.Email.ToLower() == email || u.Phone.Contains(phone))
               .GetAuthEntity();

                if (userResult == null)
                {
                    return new ApiErrorResult<AuthResult>((int)ErrorCodes.CredentialsInvalid);
                }

                var result = new AuthResult()
                {
                    Token = _tokenService.GenerateJwtToken(userResult),
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
            if (_context.Users.IsEmailExist(registerDto.Email) || _context.Users.IsEmailExist(registerDto.Phone))
            {
                return new ApiErrorResult<AuthResult>((int)ErrorCodes.DuplicatedData);
            }

            var user = new User()
            {
                FullName = registerDto.FullName,
                GenderType = registerDto.GenderType,
                Phone = registerDto.Phone,
                Dob = registerDto.Dob,
                Email = registerDto.Email,
            };

            var hashsalt = Cryptography.EncryptPassword(registerDto.Password);
            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                user.Password = hashsalt.Hash;
                user.StoredSalt = hashsalt.Salt;
            }
            await _repository.Auth.AddAsync(user);
            var userResult = _context.Users
               .Include(u => u.Roles)
               .AsNoTracking()
               .Where(u => u.Id == user.Id)
               .GetAuthEntity();
            var result = new AuthResult()
            {
                Token = _tokenService.GenerateJwtToken(userResult),
                User = userResult,
            };
            return new ApiSuccessResult<AuthResult>(result);
        }

        public async Task<ApiResult<bool>> ChangePassword(string email, UserChangePassword userChangePassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var isPasswordMatched = Cryptography.VerifyPassword(userChangePassword.OldPassword, user.StoredSalt, user.Password);
            if (isPasswordMatched)
            {
                var hashsalt = Cryptography.EncryptPassword(userChangePassword.NewPassword);
                user.Password = hashsalt.Hash;
                user.StoredSalt = hashsalt.Salt;
                _context.SaveChanges();
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>((int)ErrorCodes.CredentialsInvalid);
        }

        public async Task<ApiResult<bool>> UpdateUser(string userId, UserUpdateDto userUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            //user.FullName = userUpdateDto.FullName;
            //user.Email = userUpdateDto.Email;
            //user.GenderType = userUpdateDto.GenderType;
            //user.Dob = userUpdateDto.Dob;
            //user.Avatar = userUpdateDto.Avatar;
            //user.Phone = userUpdateDto.Phone;
            //user.IdentityID = userUpdateDto.IdentityID;
            //user.Address = userUpdateDto.Address;
            //user.Occupation = userUpdateDto.Occupation;
            //user.Country = userUpdateDto.Country;
            var updateUser = _mapper.Map(userUpdateDto, user);
            _repository.Auth.Update(updateUser);
            _context.SaveChanges();
            return new ApiSuccessResult<bool>(true);
        }
    }
}