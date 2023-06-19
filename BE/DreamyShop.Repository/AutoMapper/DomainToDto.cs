using AutoMapper;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos.Category;
using DreamyShop.Domain.Shared.Dtos.Manufacturer;
using DreamyShop.Domain.Shared.Dtos.Product;
using DreamyShop.Domain.Shared.Dtos.User;

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

            CreateMap<Product, ProductCreateDto>();

            CreateMap<Domain.Attribute, ProductAttributeDto>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Manufacturer, ManufacturerCreateUpdateDto>();

            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<ProductCategory, CategoryCreateUpdateDto>();
        }
    }
}