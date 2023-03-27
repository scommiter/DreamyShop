using AutoMapper;
using DreamyShop.Domain.Shared.AutoMapper;
using DreamyShop.Logic.Auth;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Generic;
using DreamyShop.Repository.RepositoryWrapper;

namespace DreamyShop.Api.Configurations
{
    public static class MapServicesConfig
    {
        public static void MapServices(this IServiceCollection services)
        {
            services.AddSingleton<AccessToken>();
            services.AddSingleton(AutoMapperProfile.RegisterMappings().CreateMapper());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthLogic, AuthLogic>();
        }
    }
}
