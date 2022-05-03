using System.Data;
using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Databases
{
    public class AccountsDatabase : SqlBase, IAccountsDatabase
    {
        private const string SelectQuery = @"
            select
                employee_id AS EmployeeId,
                password AS Password,
                admin AS Admin
            from
                account
            WHERE
                employee_id = @employeeId
        ";

        private const string InsertQuery = @"
            INSERT INTO
                account
            VALUES (
                @employeeId,
                @password,
                @admin
            )
        ";

        private const string UpdateQuery = @"
            UPDATE
                account
            SET
                password = @password,
                admin = @admin
            WHERE
                employee_id = @employeeId
        ";

        private const string DeleteQuery = @"
            DELETE FROM
                account
            WHERE
                employee_id = @employeeId
        ";

        public async Task<AccountModel> Obtain(string employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId, DbType.AnsiStringFixedLength);

            var account = await QueryFirst<AccountModel>(SelectQuery, parameters);
            return account;
        }

        public async Task<int> Insert(AccountModel account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", account.EmployeeId, DbType.AnsiStringFixedLength);
            parameters.Add("@password", account.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@admin", account.Admin, DbType.Boolean);

            var rowsAffected = await Execute(InsertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(AccountModel account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", account.EmployeeId, DbType.AnsiStringFixedLength);
            parameters.Add("@password", account.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@admin", account.Admin, DbType.Boolean);

            var rowsAffected = await Execute(UpdateQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Delete(string employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId, DbType.AnsiStringFixedLength);

            var rowsAffected = await Execute(DeleteQuery, parameters);
            return rowsAffected;
        }
    }
}
