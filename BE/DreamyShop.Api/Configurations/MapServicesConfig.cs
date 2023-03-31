using DreamyShop.Logic.Auth;
using DreamyShop.Logic.Auth.Security;
using DreamyShop.Logic.User;
using DreamyShop.Logic.Product;
using DreamyShop.Repository.AutoMapper;
using DreamyShop.Repository.Repositories.Auth;
using DreamyShop.Repository.Repositories.Generic;
using DreamyShop.Repository.Repositories.Product;
using DreamyShop.Repository.Repositories.User;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAuthLogic, AuthLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IProductLogic, ProductLogic>();
        }
    }
}