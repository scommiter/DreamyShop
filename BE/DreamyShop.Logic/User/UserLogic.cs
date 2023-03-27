using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Repository.Helpers;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.EntityFrameworkCore;

namespace DreamyShop.Logic.User
{
    public class UserLogic : IUserLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly AccessToken _tokenService;
        private readonly IMapper _mapper;

        public UserLogic(
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

        public async Task<ApiResult<PageResult<UserDto>>> GetAllUser(int page = 1, int limit = 10)
        {
            var userPagings = _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
            var pageResult = new PageResult<UserDto>()
            {
                Items = _mapper.Map<List<UserDto>>(userPagings),
                Totals = userPagings.Count()
            };
            return new ApiSuccessResult<PageResult<UserDto>>(pageResult);
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
            _context.SaveChanges();
            return new ApiSuccessResult<bool>(true);
        }
    }
}