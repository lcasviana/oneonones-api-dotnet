using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Oneonones.Infrastructure.Configurations
{
    public static class ServiceContainerConfiguration
    {
        public static void ConfigureApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers(options => options.Filters.Add(new GlobalExceptionFilterAttribute()));

            serviceCollection.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Oneonones API",
                Contact = new OpenApiContact
                {
                    Name = "Lucas Silvestre Viana",
                    Email = "lcasviana@gmail.com",
                    Url = new Uri("https://github.com/lcasviana"),
                },
            }));
        }

        public static void AddSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oneonones API"));
        }
    }
}
