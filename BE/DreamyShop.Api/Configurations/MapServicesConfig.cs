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
using DreamyShop.Repository.Repositories.Role;
using DreamyShop.Repository.Repositories.Category;
using DreamyShop.Logic.Role;
using DreamyShop.Logic.Category;

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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductVariantValueRepository, ProductVariantValueRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region Logic
            services.AddScoped<IAuthLogic, AuthLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IRoleLogic, RoleLogic>();
            services.AddScoped<IProductLogic, ProductLogic>();
            services.AddScoped<IManufacturerLogic, ManufacturerLogic>();
            services.AddScoped<ICategoryLogic, CategoryLogic>();
            #endregion
        }
    }
}