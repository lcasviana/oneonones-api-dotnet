namespace Oneonones.Infrastructure.Packages;

public static class SwashbuckleConfig
{
    public static void AddSwashbuckle(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c => c.SwaggerDoc("v1", new()
        {
            Version = "v1",
            Title = "Oneonones API",
            Contact = new()
            {
                Name = "Lucas Silvestre Viana",
                Email = "lcasviana@gmail.com",
                Url = new("https://github.com/lcasviana"),
            },
        }));
    }

    public static void UseSwashbuckle(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oneonones API"));
    }
}
