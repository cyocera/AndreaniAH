using ApiGeo.Configurations;
using ApiGeo.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plain.RabbitMQ;
using RabbitMQ.Client;
using System.Net;

namespace ApiGeo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string database = Configuration.GetConnectionString("UPConnectionStringsLocal");
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(database);
                options.EnableSensitiveDataLogging(true);
            });

            services.AddControllers();
            StartupConfiguration.AddHttpClients(services, Configuration);
            services.AddProjectServices(Configuration);
            services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@host.docker.internal:5672"));
            services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
                "report_exchange",
                "report_queue",
                "report.*",
                ExchangeType.Topic));
            services.AddHostedService<DataCollector>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            StartupConfiguration.InitializeDatabase(app);
        }
    }
}
