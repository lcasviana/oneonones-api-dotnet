using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

namespace Oneonones.Infrastructure.Configurations;

public static class SwashbuckleConfig
{
    public static void AddSwashbuckle(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Version = "v1", Title = "Oneonones API" });
            var xmlDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            options.IncludeXmlComments(xmlDocumentationFilePath, true);
        });
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
    }

    public static void UseSwashbuckle(this IApplicationBuilder application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Oneonones API"));
    }
}
