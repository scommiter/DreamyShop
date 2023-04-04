using AutoMapper;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Repository.RepositoryWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Logic.Role
{
    public class RoleLogic : IRoleLogic
    {
        private readonly DreamyShopDbContext _context;
        private readonly IRepositoryWrapper _repository;
        public RoleLogic(
            DreamyShopDbContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<ApiResult<bool>> AssignRole(Guid userId, List<byte> roleIds)
        {
            var user = await _repository.User.GetByIdAsync(userId);
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var rolesToAdd = roleIds.Select(r => new Domain.Role
            {
                Id = Guid.NewGuid(),
                UserID = userId,
                RoleType = r,
                ProfileUrl = "",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            });
            await _repository.Role.AddRangeAsync(rolesToAdd);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateRole(Guid userId, List<byte> roleIds)
        {
            var user = await _repository.User.GetByIdAsync(userId);
            if (user == null)
            {
                return new ApiErrorResult<bool>((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var roleTypesOfUser = _context.Roles.Where(r => r.UserID == userId).ToList();
            if(roleTypesOfUser != null)
            {
                _repository.Role.RemoveMultiple(roleTypesOfUser);
            }

            var rolesToAdd = roleIds.Select(r => new Domain.Role
            {
                Id = Guid.NewGuid(),
                UserID = userId,
                RoleType = r,
                ProfileUrl = "",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            });
            await _repository.Role.AddRangeAsync(rolesToAdd);
            _repository.Save();
            return new ApiSuccessResult<bool>(true);
        }
    }
}
