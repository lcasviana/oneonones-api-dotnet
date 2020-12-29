using HealthChecks.UI.Client;
using Meetings.Infrastructure.Extension;
using Meetings.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Serilog;
using System.IO;

namespace Meetings
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _ = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddController();
            services.AddIdentityService(Configuration);
            services.AddServicesImplementations();
            services.AddPersistencesImplementations();
            services.AddSwaggerOpenAPI();
            services.AddServiceLayer();
            services.AddVersion();
            services.AddHealthChecks();

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Basic Health Check", $"/health");
            });

            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(options => options
                 .WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            log.AddSerilog();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureSwagger();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}