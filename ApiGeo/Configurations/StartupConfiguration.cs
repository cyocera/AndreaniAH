using ApiGeo.Data.Context;
using ApiGeo.Data.UnitOfWork;
using ApiGeo.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiGeo.Configurations
{
    public static class StartupConfiguration
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
                context.Database.Migrate();
            }
        }

        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGeolocatorManager, GeolocatorManager>();
        }

        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var processMessageURL = "http://host.docker.internal:8080/";
            services.AddHttpClient("api.processMessage", configureClient =>
            {
                configureClient.BaseAddress = new Uri(processMessageURL);
                configureClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
    }
}
