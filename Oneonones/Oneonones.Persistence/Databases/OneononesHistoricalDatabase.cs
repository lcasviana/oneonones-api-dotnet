using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Databases
{
    public class OneononesHistoricalDatabase : SqlBase, IOneononesHistoricalDatabase
    {
        private const string obtainAllQuery = @"
            SELECT
                leader_email AS LeaderEmail,
                led_email AS LedEmail,
                occurrence AS Occurrence,
                commentary AS Commentary
            FROM
                oneonones_historical
        ";

        private const string obtainByEmployeeQuery = @"
            SELECT
                leader_email AS LeaderEmail,
                led_email AS LedEmail,
                occurrence AS Occurrence,
                commentary AS Commentary
            FROM
                oneonones_historical
            WHERE
                leader_email = @email
                OR led_email = @email
        ";

        private const string obtainByPairQuery = @"
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

        private const string obtainByPairLastQuery = @"
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
	            AND occurrence = (
		            SELECT
			            max(occurrence)
		            FROM
			            oneonones_historical
		            WHERE
			            leader_email = @leaderEmail
			            AND led_email = @ledEmail
	            )
        ";

        private const string obtainByPairOccurrenceQuery = @"
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
                AND occurrence = @occurrence
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

        public async Task<IList<OneononeHistoricalModel>> ObtainAll()
        {
            var oneononeHistoricalModelList = await Query<OneononeHistoricalModel>(obtainAllQuery);
            return oneononeHistoricalModelList;
        }

        public async Task<IList<OneononeHistoricalModel>> ObtainByEmployee(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email, DbType.AnsiString);

            var oneononeHistoricalModelList = await Query<OneononeHistoricalModel>(obtainByEmployeeQuery, parameters);
            return oneononeHistoricalModelList;
        }

        public async Task<IList<OneononeHistoricalModel>> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);

            var oneononeHistoricalModelList = await Query<OneononeHistoricalModel>(obtainByPairQuery, parameters);
            return oneononeHistoricalModelList;
        }

        public async Task<OneononeHistoricalModel> ObtainByPairLast(string leaderEmail, string ledEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);

            var oneononeHistoricalModel = await QueryFirst<OneononeHistoricalModel>(obtainByPairLastQuery, parameters);
            return oneononeHistoricalModel;
        }

        public async Task<OneononeHistoricalModel> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);
            parameters.Add("@occurrence", occurrence, DbType.DateTime);

            var oneononeHistoricalModel = await QueryFirst<OneononeHistoricalModel>(obtainByPairOccurrenceQuery, parameters);
            return oneononeHistoricalModel;
        }

        public async Task<int> Insert(OneononeHistoricalModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@occurrence", oneonone.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", oneonone.Commentary, DbType.AnsiString);

            var rowsAffected = await Execute(insertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(OneononeHistoricalModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@occurrence", oneonone.Occurrence, DbType.DateTime);
            parameters.Add("@commentary", oneonone.Commentary, DbType.AnsiString);

            var rowsAffected = await Execute(updateQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);
            parameters.Add("@occurrence", occurrence, DbType.DateTime);

            var rowsAffected = await Execute(deleteQuery, parameters);
            return rowsAffected;
        }
    }
}