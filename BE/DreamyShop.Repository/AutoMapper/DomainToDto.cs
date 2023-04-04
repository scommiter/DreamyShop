using AutoMapper;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;

namespace DreamyShop.Repository.AutoMapper
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<User, UserDto>()
                .ForPath(u => u.RoleTypes,
                           act => act.MapFrom(src => src.Roles.Select(e => e.RoleType)));
            CreateMap<User, UserUpdateDto>();
            CreateMap<Product, ProductCreateUpdateDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(pt => pt.CategoryName, opt => 
                    opt.MapFrom(src => src.ProductCategory.Name))
                .ForMember(pt => pt.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Manufacturer.Name));
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Manufacturer, ManufacturerCreateUpdateDto>();
        }
    }
}