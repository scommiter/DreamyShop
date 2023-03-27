using AutoMapper;

namespace DreamyShop.Domain.Shared.AutoMapper
{
    public class AutoMapperProfile
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDto());
                cfg.AddProfile(new DtoToDomain());
            });
        }
    }
}