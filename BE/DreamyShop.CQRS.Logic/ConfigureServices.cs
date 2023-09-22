using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DreamyShop.CQRS.Logic
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
