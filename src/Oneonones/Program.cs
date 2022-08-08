using Oneonones.Infrastructure;
using Oneonones.Infrastructure.Packages;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwashbuckle();
builder.Services.AddValidators();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwashbuckle();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
