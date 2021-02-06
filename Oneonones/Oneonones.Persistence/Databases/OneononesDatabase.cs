using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contract;
using Oneonones.Persistence.Models;
using System.Data;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Databases
{
    public class OneononesDatabase : SqlBase, IOneononesDatabase
    {
        private const string obtainQuery = @"
            SELECT
                leader_email AS LeaderEmail,
                led_email AS LedEmail,
                frequency_type AS FrequencyType
            FROM
                oneonones
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
        ";

        private const string insertQuery = @"
            INSERT INTO
                oneonones
            VALUES (
                @leaderEmail,
                @ledEmail,
                @frequencyType
            )
        ";

        private const string updateQuery = @"
            UPDATE
                oneonones
            SET
                frequency_type = @frequencyType
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
        ";

        private const string deleteQuery = @"
            DELETE FROM
                oneonones
            WHERE
                leader_email = @leaderEmail
                AND led_email = @ledEmail
        ";

        public async Task<OneononeModel> Obtain(string leaderEmail, string ledEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);

            var oneononeModel = await QueryFirst<OneononeModel>(obtainQuery, parameters);
            return oneononeModel;
        }

        public async Task Insert(OneononeModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@frequencyType", oneonone.FrequencyType, DbType.Int32);

            await Execute(insertQuery, parameters);
        }

        public async Task Update(OneononeModel oneonone)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", oneonone.LeaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", oneonone.LedEmail, DbType.AnsiString);
            parameters.Add("@frequencyType", oneonone.FrequencyType, DbType.Int32);

            await Execute(updateQuery, parameters);
        }

        public async Task Delete(string leaderEmail, string ledEmail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@leaderEmail", leaderEmail, DbType.AnsiString);
            parameters.Add("@ledEmail", ledEmail, DbType.AnsiString);


            await Execute(deleteQuery, parameters);
        }
    }
}