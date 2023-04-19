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

            //CreateMap<Product, ProductDto>()
            //    .ForMember(pt => pt.CategoryName, opt =>
            //        opt.MapFrom(src => src.ProductCategory.Name))
            //    .ForMember(pt => pt.ManufacturerName, opt =>
            //        opt.MapFrom(src => src.Manufacturer.Name))
            //    .ForMember(dest => dest.ProductAttributeDisplayDtos, opt => opt.MapFrom(src => src.ProductVariants
            //        .Select(variant => new ProductAttributeDisplayDto
            //        {
            //            AttributeName = string.Join(" ", src.ProductVariantValues.GroupBy(p => p.ProductVariantId)
            //                            .Select(g => String.Join(" ", g.Select(p =>
            //                            src.ProductAttributeValues.FirstOrDefault(pav => pav.Id == p.ProductAttributeValueId)?.Value))).ToList(),
            //            Quantity = variant.Quantity,
            //            Price = variant.Price
            //        }).ToList()));

            CreateMap<Product, ProductDto>()
                .ForMember(pt => pt.CategoryName, opt =>
                    opt.MapFrom(src => src.ProductCategory.Name))
                .ForMember(pt => pt.ManufacturerName, opt =>
                    opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ProductAttributeDisplayDtos, opt => opt.MapFrom(src => src.ProductVariants
                    .Select(variant => src.ProductVariantValues.GroupBy(p => p.ProductVariantId)
                        .Select(variantValue => new ProductAttributeDisplayDto
                        {
                            AttributeName = string.Join(" ", variantValue.Select(p =>
                                            src.ProductAttributeValues.FirstOrDefault(pav => pav.Id == p.ProductAttributeValueId)!.Value).ToString()),
                            Quantity = variant.Quantity,
                            Price = variant.Price
                        })
                    ))
                );


            //CreateMap<Product, ProductDto>()
            //    .ForMember(pt => pt.CategoryName, opt =>
            //        opt.MapFrom(src => src.ProductCategory.Name))
            //    .ForMember(pt => pt.ManufacturerName, opt =>
            //        opt.MapFrom(src => src.Manufacturer.Name))
            //    .ForMember(dest => dest.ProductAttributeDisplayDtos, opt => opt.MapFrom(src =>
            //        src.ProductVariants
            //            .Select(variant =>
            //                new
            //                {
            //                    variant.Quantity,
            //                    variant.Price,
            //                    ProductAttributeValueStrings = src.ProductAttributeValues
            //                        .Join(src.ProductVariantValues, pat => pat.Id, pvvt => pvvt.ProductAttributeValueId,
            //                            (pat, pvvt) => new { pat, pvvt })
            //                        .Where(joined => joined.pvvt.ProductId == src.Id)
            //                        .Select(joined => joined.pat)
            //                })
            //            .GroupBy(p => string.Join(",", p.ProductAttributeValueStrings.Select(pat => pat.Id)))
            //            .Select(g => new ProductAttributeDisplayDto
            //            {
            //                AttributeName = string.Join(", ", g.SelectMany(p => p.ProductAttributeValueStrings).Select(pat => pat.Value)),
            //                Quantity = g.Sum(p => p.Quantity),
            //                Price = g.Sum(p => p.Price)
            //            })
            //            .ToList()));


            CreateMap<ProductVariant, ProductVariantDto>();

            CreateMap<Domain.Attribute, ProductAttributeDto>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Manufacturer, ManufacturerCreateUpdateDto>();

            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<ProductCategory, CategoryCreateUpdateDto>();
        }
    }
}