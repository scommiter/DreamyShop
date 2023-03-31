using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Logic.Conditions;
using DreamyShop.Repository.Helpers;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.Logic.User
{
    public class UserLogic : IUserLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UserLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<PageResult<UserDto>>> GetAllUser(int page = 1, int limit = 10)
        {
            var userPagings = _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.DateCreated).ToList();
            var pageResult = new PageResult<UserDto>()
            {
                Items = _mapper.Map<List<UserDto>>(GetPagingUsers(userPagings, page, limit)),
                Totals = userPagings.Count()
            };
            return new ApiSuccessResult<PageResult<UserDto>>(pageResult);
        }

        private List<Domain.User> GetPagingUsers(List<Domain.User> userList, int page, int limit)
        {
            return userList.Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
        }

        public async Task<ApiResult<IList<UserDto>>> Search(SearchUserCondition condition)
        {
            var usersResult = _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.DateCreated).ToList();
            if (condition == null)
            {
                return new ApiSuccessResult<IList<UserDto>>(GetAllUser().Result.Result.Items);
            }
            if(condition.FullName != null)
            {
                usersResult = usersResult.Where(u => u.FullName.ToLower() == condition.FullName.ToLower()).ToList();
            }
            if (condition.GenderType != null)
            {
                usersResult = usersResult.Where(u => u.GenderType == condition.GenderType).ToList();
            }
            if (condition.Dob != null)
            {
                usersResult = usersResult.Where(u => u.Dob == condition.Dob).ToList();
            }
            if (condition.Email != null)
            {
                usersResult = usersResult.Where(u => u.Email == condition.Email.RemoveAllWhiteSpace()).ToList();
            }
            if (condition.Phone != null)
            {
                usersResult = usersResult.Where(u => u.Phone == condition.Phone).ToList();
            }
            if (condition.IdentityID != null)
            {
                usersResult = usersResult.Where(u => u.IdentityID == condition.IdentityID).ToList();
            }
            if (condition.RoleTypes != null && condition.RoleTypes.Count > 0)
            {
                usersResult = usersResult.Where(u => condition.RoleTypes.All(role => u.Roles.Any(r => r.RoleType == role))).ToList();
            }
            var userPagingsResult = GetPagingUsers(usersResult, 1, 10);
            return new ApiSuccessResult<IList<UserDto>>(_mapper.Map<List<UserDto>>(userPagingsResult));
        }

        public async Task<ApiResult<bool>> UpdateUser(string userId, UserUpdateDto userUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            if (_context.Users.IsEmailExist(userUpdateDto.Email) || _context.Users.IsPhoneExist(userUpdateDto.Phone))
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DuplicatedData);
            }
            var updateUser = _mapper.Map(userUpdateDto, user);
            _repository.Auth.Update(updateUser);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteUser(string userId)
        {
            if(userId == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            bool allowedDelete = _context.Users.Any(u => u.Roles.Any(r => r.RoleType == 1));
            if(!allowedDelete)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.UserIsUnauthorized);
            }
            _repository.User.Remove(Guid.Parse(userId));
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}