using Microsoft.EntityFrameworkCore;
using Npgsql;
using Oneonones.Repositories.Context;

namespace Oneonones.Infrastructure.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseUrl = configuration["DATABASE_URL"];
        var databaseUri = new Uri(databaseUrl!);
        var userInfo = databaseUri.UserInfo.Split(':');

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
        };

        var connectionString = connectionStringBuilder.ToString();

        services.AddDbContext<OneononeContext>(options => options.UseNpgsql(connectionString));
    }
}
