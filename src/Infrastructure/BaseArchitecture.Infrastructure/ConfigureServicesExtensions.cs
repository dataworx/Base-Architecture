using BaseArchitecture.Domain.Data;
using BaseArchitecture.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace BaseArchitecture.Infrastructure
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, DataContext>();
            return services;
        }
    }
}