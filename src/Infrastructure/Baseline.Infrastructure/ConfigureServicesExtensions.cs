using Baseline.Domain.Data;
using Baseline.Infrastructure.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Baseline.Infrastructure
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