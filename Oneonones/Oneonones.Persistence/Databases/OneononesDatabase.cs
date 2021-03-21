using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Databases
{
    public class OneononesDatabase : SqlBase, IOneononesDatabase
    {
        private const string selectQuery = @"
            SELECT
                id AS Id,
                leader_id AS LeaderId,
                led_id AS LedId,
                frequency AS Frequency
            FROM
                oneonone
        ";

        private const string whereById = @"
            WHERE
                id = @id
        ";

        private const string whereByEmployee = @"
            WHERE
                leader_id = @id
                OR led_id = @id
        ";

        private const string whereByPair = @"
            WHERE
                leader_id = @leaderId
                AND led_id = @ledId
        ";

        private const string insertQuery = @"
            INSERT INTO
                oneonone
            VALUES (
                @id,
                @leaderId,
                @ledId,
                @frequency
            )
        ";

        private const string updateQuery = @"
            UPDATE
                oneonone
            SET
                leader_id = @leaderId,
                led_id = @ledId,
                frequency = @frequency
            WHERE
                id = @id
        ";

        private const string deleteQuery = @"
            DELETE FROM
                oneonone
            WHERE
                id = @id
        ";

        public async Task<IList<OneononeModel>> Obtain()
        {
            var oneononeList = await Query<OneononeModel>(selectQuery);
            return oneononeList;
        }

        public async Task<OneononeModel> Obtain(string id)
        {
            var query = selectQuery + whereById;
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.AnsiStringFixedLength);

            var oneonone = await QueryFirst<OneononeModel>(query, parameters);
            return oneonone;
        }

        public async Task<IList<OneononeModel>> ObtainByEmployee(string id)
        {
            var query = selectQuery + whereByEmployee;
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.AnsiStringFixedLength);

            var oneononeList = await Query<OneononeModel>(query, parameters);
            return oneononeList;
        }

        public async Task<OneononeModel> ObtainByPair(string leaderId, string ledId)
        {
            var query = selectQuery + whereByPair;
            var parameters = new DynamicParameters();
            parameters.Add("@leaderId", leaderId, DbType.AnsiString);
            parameters.Add("@ledId", ledId, DbType.AnsiString);

            var oneonone = await QueryFirst<OneononeModel>(query, parameters);
            return oneonone;
        }

        public async Task<int> Insert(OneononeModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", oneonone.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@leaderId", oneonone.LeaderId, DbType.AnsiString);
            parameters.Add("@ledId", oneonone.LedId, DbType.AnsiString);
            parameters.Add("@frequency", oneonone.Frequency, DbType.Int32);

            var rowsAffected = await Execute(insertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(OneononeModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", oneonone.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@leaderId", oneonone.LeaderId, DbType.AnsiString);
            parameters.Add("@ledId", oneonone.LedId, DbType.AnsiString);
            parameters.Add("@frequency", oneonone.Frequency, DbType.Int32);

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