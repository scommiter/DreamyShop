using AutoMapper;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using Microsoft.Data.SqlClient;

namespace DreamyShop.Repository.AutoMapper
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
            CreateMap<UserDto, User>()
                .ForMember(u => u.Password, option => option.Ignore())
                .ForMember(u => u.StoredSalt, option => option.Ignore())
                .ForMember(u => u.IdentityID, option => option.Ignore())
                .ForMember(u => u.Occupation, option => option.Ignore())
                .ForMember(u => u.Country, option => option.Ignore())
                .ForPath(u => u.Roles, act => act.MapFrom(src => src.RoleTypes));
            CreateMap<UserUpdateDto, User>()
                .ForMember(u => u.Id, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.Password, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.StoredSalt, option => option.DoNotUseDestinationValue())
                .ForMember(u => u.Roles, option => option.DoNotUseDestinationValue());
        }
    }
}