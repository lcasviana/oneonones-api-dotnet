using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Base
{
    public abstract class SqlBase
    {
        private SqlConnection SqlConnection => new SqlConnection(Environment.GetEnvironmentVariable("DatabaseConnectionString"));

        protected async Task<IList<T>> Query<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = SqlConnection;
            IEnumerable<T> results = await connection.QueryAsync<T>(query, parameters, null, timeout);
            return results.ToList();
        }

        protected async Task<T> QueryFirst<T>(string query, object parameters = null, int? timeout = null)
        {
            using var connection = SqlConnection;
            T result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters, null, timeout);
            return result;
        }

        protected async Task<int> Execute(string query, object parameters = null, int? timeout = null)
        {
            using var connection = SqlConnection;
            int rowsAffected = await connection.ExecuteAsync(query, parameters, null, timeout);
            return rowsAffected;
        }
    }
}