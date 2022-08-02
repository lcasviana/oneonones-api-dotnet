using Microsoft.EntityFrameworkCore;
using Oneonones.Infrastructure;
using Oneonones.Infrastructure.Packages;
using Oneonones.Repositories.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(prefix: "PostgreSQL_");
string connectionString = builder.Configuration.GetConnectionString("PostgreSQL")
    .Replace("<host>", builder.Configuration["PostgreSQL_Host"])
    .Replace("<port>", builder.Configuration["PostgreSQL_Port"])
    .Replace("<username>", builder.Configuration["PostgreSQL_Username"])
    .Replace("<username>", builder.Configuration["PostgreSQL_Username"])
    .Replace("<password>", builder.Configuration["PostgreSQL_Password"])
    .Replace("<database>", builder.Configuration["PostgreSQL_Database"]);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwashbuckle();
builder.Services.AddValidators();
builder.Services.AddServices();
builder.Services.AddDbContext<OneononeContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwashbuckle();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
