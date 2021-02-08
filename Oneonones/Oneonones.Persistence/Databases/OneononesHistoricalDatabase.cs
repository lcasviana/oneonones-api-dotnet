using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts;
using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Databases
{
    public class OneononesHistoricalDatabase : SqlBase, IOneononesHistoricalDatabase
    {
        private const string obtainQuery = @"
            SELECT
                leader_email AS LeaderEmail,
                led_email AS LedEmail,
                occurrence AS Occurrence,
                commentary AS Commentary
            FROM
                oneonones_historical
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
        ";

        private const string insertQuery = @"
            INSERT INTO
                oneonones_historical
            VALUES (
                @leaderEmail,
                @ledEmail,
                @occurrence,
                @commentary
            )
        ";

        private const string updateQuery = @"
            UPDATE
                oneonones_historical
            SET
                commentary = @commentary
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
                AND occurrence = @occurrence
        ";

        private const string deleteQuery = @"
            DELETE FROM
                oneonones_historical
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
                AND occurrence = @occurrence
        ";

        public async Task<IList<OneononeHistoricalModel>> Obtain(string leaderEmail, string ledEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);

            var oneononeHistoricalModel = await Query<OneononeHistoricalModel>(obtainQuery, parameters);
            return oneononeHistoricalModel.ToList();
        }

        public async Task Insert(OneononeHistoricalModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@occurrence", oneonone.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", oneonone.Commentary, DbType.AnsiString);

            await Execute(insertQuery, parameters);
        }

        public async Task Update(OneononeHistoricalModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@occurrence", oneonone.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", oneonone.Commentary, DbType.AnsiString);

            await Execute(updateQuery, parameters);
        }

        public async Task Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);
            parameters.Add("@occurrence", occurrence, DbType.DateTime);

            await Execute(deleteQuery, parameters);
        }
    }
}