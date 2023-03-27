using AutoMapper;

namespace DreamyShop.Repository.AutoMapper
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