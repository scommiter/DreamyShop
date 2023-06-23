using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.User;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Logic.Conditions;
using DreamyShop.Repository.Helpers;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.AspNetCore.Http;
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

        public async Task<ApiResult<PageResult<UserDto>>> GetAllUser(PagingRequest pagingRequest)
        {
            var userPagings = _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.DateCreated).ToList();
            var pageResult = new PageResult<UserDto>()
            {
                Items = _mapper.Map<List<UserDto>>(GetPagingUsers(userPagings, pagingRequest)),
                Totals = userPagings.Count()
            };
            return new ApiSuccessResult<PageResult<UserDto>>(pageResult);
        }

        private List<Domain.User> GetPagingUsers(List<Domain.User> userList, PagingRequest pagingRequest)
        {
            return userList.Skip((pagingRequest.Page - 1) * pagingRequest.Limit)
                                .Take(pagingRequest.Limit)
                                .ToList();
        }

        public async Task<ApiResult<IList<UserDto>>> Search(SearchUserCondition condition)
        {
            var pagingRequest = new PagingRequest() { Page = 1, Limit = 10 };
            var usersResult = _context.Users
                .Include(u => u.Roles)
                .OrderByDescending(u => u.DateCreated).ToList();
            if (condition == null)
            {
                return new ApiSuccessResult<IList<UserDto>>(GetAllUser(pagingRequest).Result.Result.Items);
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
            var userPagingsResult = GetPagingUsers(usersResult, pagingRequest);
            return new ApiSuccessResult<IList<UserDto>>(_mapper.Map<List<UserDto>>(userPagingsResult));
        }

        public async Task<ApiResult<bool>> UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            //if (_context.Users.IsEmailExist(userUpdateDto.Email) || _context.Users.IsPhoneExist(userUpdateDto.Phone))
            //{
            //    return new ApiErrorResult<bool>((int)ErrorCodes.DuplicatedData);
            //}
            var updateUser = _mapper.Map(userUpdateDto, user);
            if (!String.IsNullOrEmpty(updateUser.Avatar))
            {
                var fileName = updateUser.FullName.RemoveAllWhiteSpace().ToLower() + ".png";
                AddImage(updateUser.Avatar, fileName);
                updateUser.Avatar = fileName;
            }

            _repository.Auth.Update(updateUser);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        private void AddImage(string fileContext, string fileName)
        {
            string base64Data = fileContext.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(base64Data);
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, Path.GetFileNameWithoutExtension(fileName), $"{fileName}.png");
                var pathToSave = Directory.GetCurrentDirectory().Replace("BE\\DreamyShop.Api", "FE\\src\\assets\\ImageProducts");
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                if (file.Length > 0)
                {
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = fileName;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
        }


        public async Task<ApiResult<bool>> DeleteUser(string userId)
        {
            if(userId == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            bool allowedDelete = _context.Users.Any(u => u.Roles.Any(r => r.RoleType == 1));
            if(!allowedDelete)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.UserIsUnauthorized);
            }
            _repository.User.Remove(int.Parse(userId));
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}