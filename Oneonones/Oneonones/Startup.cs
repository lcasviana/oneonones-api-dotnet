using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Oneonones.Infrastructure.Configurations;
using Serilog;
using System.IO;

namespace Oneonones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _ = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApi();
            services.ResolveDependencyInjections();
            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.AddSwagger();
            log.AddSerilog();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}