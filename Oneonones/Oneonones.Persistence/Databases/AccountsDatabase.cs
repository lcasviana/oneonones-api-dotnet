using System.Data;
using System.Threading.Tasks;
using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Databases
{
    public class AccountsDatabase : SqlBase, IAccountsDatabase
    {
        private const string selectQuery = @"
            select
                employee_id AS EmployeeId,
                password AS Password,
                admin AS Admin
            from
                account
            WHERE
                employee_id = @employeeId
        ";

        private const string insertQuery = @"
            INSERT INTO
                account
            VALUES (
                @employeeId,
                @password,
                @admin
            )
        ";

        private const string updateQuery = @"
            UPDATE
                account
            SET
                password = @password,
                admin = @admin
            WHERE
                employee_id = @employeeId
        ";

        private const string deleteQuery = @"
            DELETE FROM
                account
            WHERE
                employee_id = @employeeId
        ";

        public async Task<AccountModel> Obtain(string employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId, DbType.AnsiStringFixedLength);

            var account = await QueryFirst<AccountModel>(selectQuery, parameters);
            return account;
        }

        public async Task<int> Insert(AccountModel account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", account.EmployeeId, DbType.AnsiStringFixedLength);
            parameters.Add("@password", account.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@admin", account.Admin, DbType.Boolean);

            var rowsAffected = await Execute(insertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(AccountModel account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", account.EmployeeId, DbType.AnsiStringFixedLength);
            parameters.Add("@password", account.Password, DbType.AnsiStringFixedLength);
            parameters.Add("@admin", account.Admin, DbType.Boolean);

            var rowsAffected = await Execute(updateQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Delete(string employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId, DbType.AnsiStringFixedLength);

            var rowsAffected = await Execute(deleteQuery, parameters);
            return rowsAffected;
        }
    }
}