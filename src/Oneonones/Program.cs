using Oneonones.Infrastructure.Configurations;
using Oneonones.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new DomainExceptionFilterAttribute());
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin()));
builder.Services.AddSwashbuckle();
builder.Services.AddValidators();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();

var app = builder.Build();

var appUrl = app.Environment.IsDevelopment() ? $"https://*:53623" : $"http://*:{builder.Configuration["PORT"]}";
app.Urls.Add(appUrl);

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwashbuckle();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
