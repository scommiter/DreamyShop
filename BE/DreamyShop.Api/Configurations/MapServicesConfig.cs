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
using DreamyShop.Repository.Repositories.Manufacturer;
using DreamyShop.Logic.Manufacturer;

namespace DreamyShop.Api.Configurations
{
    public static class MapServicesConfig
    {
        public static void MapServices(this IServiceCollection services)
        {
            services.AddSingleton<AccessToken>();
            services.AddSingleton(AutoMapperProfile.RegisterMappings().CreateMapper());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region REPOSITORY
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeDateTimeRepository, ProductAttributeDateTimeRepository>();
            services.AddScoped<IProductAttributeDecimalRepository, ProductAttributeDecimalRepository>();
            services.AddScoped<IProductAttributeIntRepository, ProductAttributeIntRepository>();
            services.AddScoped<IProductAttributeTextRepository, ProductAttributeTextRepository>();
            services.AddScoped<IProductAttributeVarcharRepository, ProductAttributeVarcharRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            #endregion

            #region Logic
            services.AddScoped<IAuthLogic, AuthLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IProductLogic, ProductLogic>();
            services.AddScoped<IManufacturerLogic, ManufacturerLogic>();
            #endregion
        }
    }
}