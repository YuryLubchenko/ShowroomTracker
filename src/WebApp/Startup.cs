using System;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Database;
using DomainModel.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services;
using WebApp.Settings;

namespace WebApp
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
            RegisterSettings(services);

            DatabaseDependencyRegistrar.RegisterServices(services, Configuration);
            ServicesDependencyRegistrar.RegisterServices(services);

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApp", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PreStart(app.ApplicationServices).GetAwaiter().GetResult();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void RegisterSettings(IServiceCollection services)
        {
            var showroomSettings = new ShowroomSettings();
            Configuration.Bind("ShowroomSettings", showroomSettings);
            services.AddSingleton<IShowroomSettings>(showroomSettings);
        }

        private static async Task PreStart(IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                var t = Assembly.GetEntryAssembly()?.GetName().Version;

                var netCoreVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

                logger.LogInformation(
                    $"Starting Showroom Tracker Service: runtime version:{netCoreVersion}; app version:{t}");

                logger.LogInformation("Initializing database");

                await services.MigrateDbToLatestVersionAsync();

                logger.LogInformation("Database initialization done");
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Failed to initialize database");
                throw;
            }
        }
    }
}