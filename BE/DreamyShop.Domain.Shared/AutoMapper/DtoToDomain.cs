using AutoMapper;
using DreamyShop.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamyShop.Domain.Shared.AutoMapper
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForMember(u => u.Id, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.Password, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.StoredSalt, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.Roles, option => option.DoNotUseDestinationValue());
        }
    }
}
