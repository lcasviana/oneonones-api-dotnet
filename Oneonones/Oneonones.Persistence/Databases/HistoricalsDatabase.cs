using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Databases
{
    public class HistoricalsDatabase : SqlBase, IHistoricalsDatabase
    {
        private const string selectQuery = @"
            SELECT
                id AS Id,
                leader_id AS LeaderId,
                led_id AS LedId,
                occurrence AS Occurrence,
                commentary AS Commentary
            FROM
                historical
        ";

        private const string whereById = @"
            WHERE
                id = @id
        ";

        private const string whereByEmployee = @"
            WHERE
                leader_id = @email
                OR led_id = @email
        ";

        private const string whereByPair = @"
            WHERE
                leader_id = @leaderId
                AND led_id = @ledId
        ";

        private const string insertQuery = @"
            INSERT INTO
                historical
            VALUES (
                @id,
                @leaderId,
                @ledId,
                @occurrence,
                @commentary
            )
        ";

        private const string updateQuery = @"
            UPDATE
                historical
            SET
                leader_id = @leaderId,
                led_id = @ledId,
                occurrence = @occurrence,
                commentary = @commentary
            WHERE
                id = @id
        ";

        private const string deleteQuery = @"
            DELETE FROM
                historical
            WHERE
                id = @id
        ";

        public async Task<IList<HistoricalModel>> Obtain()
        {
            var historicalList = await Query<HistoricalModel>(selectQuery);
            return historicalList;
        }

        public async Task<HistoricalModel> Obtain(string id)
        {
            var query = selectQuery + whereById;
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.AnsiStringFixedLength);

            var historical = await QueryFirst<HistoricalModel>(query, parameters);
            return historical;
        }

        public async Task<IList<HistoricalModel>> ObtainByEmployee(string email)
        {
            var query = selectQuery + whereByEmployee;
            var parameters = new DynamicParameters();
            parameters.Add("@email", email, DbType.AnsiString);

            var historicalList = await Query<HistoricalModel>(query, parameters);
            return historicalList;
        }

        public async Task<IList<HistoricalModel>> ObtainByPair(string leaderId, string ledId)
        {
            var query = selectQuery + whereByPair;
            var parameters = new DynamicParameters();
            parameters.Add("@leaderId", leaderId, DbType.AnsiString);
            parameters.Add("@ledId", ledId, DbType.AnsiString);

            var historicalList = await Query<HistoricalModel>(query, parameters);
            return historicalList;
        }

        public async Task<int> Insert(HistoricalModel historical)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", historical.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@leaderId", historical.LeaderId, DbType.AnsiString);
            parameters.Add("@ledId", historical.LedId, DbType.AnsiString);
            parameters.Add("@occurrence", historical.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", historical.Commentary, DbType.AnsiString);

            var rowsAffected = await Execute(insertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(HistoricalModel historical)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", historical.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@leaderId", historical.LeaderId, DbType.AnsiString);
            parameters.Add("@ledId", historical.LedId, DbType.AnsiString);
            parameters.Add("@occurrence", historical.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", historical.Commentary, DbType.AnsiString);

            var rowsAffected = await Execute(updateQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Delete(string id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.AnsiStringFixedLength);

            var rowsAffected = await Execute(deleteQuery, parameters);
            return rowsAffected;
        }
    }
}