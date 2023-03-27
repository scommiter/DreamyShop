using AutoMapper;
using DreamyShop.Domain.Shared.Dtos;

namespace DreamyShop.Domain.Shared.AutoMapper
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}