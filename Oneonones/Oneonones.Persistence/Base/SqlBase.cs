using Dapper;
using Npgsql;

namespace Oneonones.Persistence.Base
{
    public abstract class SqlBase
    {
        protected static async Task<IList<T>> Query<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            IEnumerable<T> results = await connection.QueryAsync<T>(query, parameters, null, timeout);
            await connection.CloseAsync();
            return results.ToList();
        }

        protected static async Task<T> QueryFirst<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            T result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters, null, timeout);
            await connection.CloseAsync();
            return result;
        }

        protected static async Task<int> Execute(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            int rowsAffected = await connection.ExecuteAsync(query, parameters, null, timeout);
            await connection.CloseAsync();
            return rowsAffected;
        }
    }
}
