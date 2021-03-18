using Dapper;
using Oneonones.Persistence.Base;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Databases
{
    public class EmployeesDatabase : SqlBase, IEmployeesDatabase
    {
        private const string selectQuery = @"
            SELECT
                id AS Id,
                email AS Email,
                name AS Name
            FROM
                employee
        ";

        private const string whereById = @"
            WHERE
                id = @id
        ";

        private const string whereByEmail = @"
            WHERE
                email = @email
        ";

        private const string insertQuery = @"
            INSERT INTO
                employee
            VALUES (
                @id,
                @email,
                @name
            )
        ";

        private const string updateQuery = @"
            UPDATE
                employee
            SET
                email = @email,
                name = @name
            WHERE
                id = @id
        ";

        private const string deleteQuery = @"
            DELETE FROM
                employee
            WHERE
                id = @id
        ";

        public async Task<IList<EmployeeModel>> Obtain()
        {
            var employeeList = await Query<EmployeeModel>(selectQuery);
            return employeeList;
        }

        public async Task<EmployeeModel> Obtain(string id)
        {
            var query = selectQuery + whereById;
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.AnsiStringFixedLength);

            var employee = await QueryFirst<EmployeeModel>(query, parameters);
            return employee;
        }

        public async Task<EmployeeModel> ObtainByEmail(string email)
        {
            var query = selectQuery + whereByEmail;
            var parameters = new DynamicParameters();
            parameters.Add("@email", email, DbType.AnsiString);

            var employee = await QueryFirst<EmployeeModel>(query, parameters);
            return employee;
        }

        public async Task<int> Insert(EmployeeModel employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", employee.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@email", employee.Email, DbType.AnsiString);
            parameters.Add("@name", employee.Name, DbType.AnsiString);

            var rowsAffected = await Execute(insertQuery, parameters);
            return rowsAffected;
        }

        public async Task<int> Update(EmployeeModel employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", employee.Id, DbType.AnsiStringFixedLength);
            parameters.Add("@email", employee.Email, DbType.AnsiString);
            parameters.Add("@name", employee.Name, DbType.AnsiString);

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