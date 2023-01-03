using Geocodificador.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geocodificador
{
    public static class StartupConfiguration
    {
        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGeoProcessorManager, GeoProcessorManager>();
        }
    }
}
