using Dreamy.Logic.Auth.Security;
using Dreamy.Logic.Auth;
using Dreamy.Repository.Authen;
using Dreamy.Repository.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DREAMYMVC.Configurations
{
    public static class MapServicesConfig
    {
        public static void MapServices(this IServiceCollection services)
        {
            IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("DreamyShopDBContext");
            services.AddSingleton<AccessToken>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDbConnection>((sp) => new SqlConnection(connectionString));

            #region REPOSITORY
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            //services.AddScoped<IProductImageRepository, ProductImageRepository>();
            //services.AddScoped<IProductVariantValueRepository, ProductVariantValueRepository>();
            //services.AddScoped<IAttributeRepository, AttributeRepository>();
            //services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            //services.AddScoped<IProductAttributeValueRepository, ProductAttributeValueRepository>();
            //services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ICartRepository, CartRepository>();
            //services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            //services.AddScoped<IBillRepository, BillRepository>();
            //services.AddScoped<IBillDetailRepository, BillDetailRepository>();
            #endregion

            #region Logic
            services.AddScoped<IAuthLogic, AuthLogic>();
            //services.AddScoped<IUserLogic, UserLogic>();
            //services.AddScoped<IRoleLogic, RoleLogic>();
            //services.AddScoped<IProductLogic, ProductLogic>();
            //services.AddScoped<IManufacturerLogic, ManufacturerLogic>();
            //services.AddScoped<ICategoryLogic, CategoryLogic>();
            //services.AddScoped<IReportLogic, ReportLogic>();
            //services.AddScoped<ICartLogic, CartLogic>();
            //services.AddScoped<IBillLogic, BillLogic>();
            //services.AddScoped<IChartLogic, ChartLogic>();
            #endregion
        }
    }
}
