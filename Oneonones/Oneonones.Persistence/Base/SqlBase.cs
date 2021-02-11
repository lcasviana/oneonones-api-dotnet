using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Base
{
    public abstract class SqlBase
    {
        protected async Task<IList<T>> Query<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            IEnumerable<T> results = await connection.QueryAsync<T>(query, parameters, null, timeout);
            await connection.CloseAsync();
            return results.ToList();
        }

        protected async Task<T> QueryFirst<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            T result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters, null, timeout);
            await connection.CloseAsync();
            return result;
        }

        protected async Task<int> Execute(string query, object parameters = null, int? timeout = null)
        {
            using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
            await connection.OpenAsync();
            int rowsAffected = await connection.ExecuteAsync(query, parameters, null, timeout);
            await connection.CloseAsync();
            return rowsAffected;
        }
    }
}