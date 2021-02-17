using Employees.Persistence.Mapping;
using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IEmployeesDatabase employeesDatabase;

        public EmployeesRepository(IEmployeesDatabase employeesDatabase)
        {
            this.employeesDatabase = employeesDatabase;
        }

        public async Task<IList<EmployeeEntity>> ObtainAll()
        {
            var employeeModelList = await employeesDatabase.ObtainAll();
            var employeeEntityList = employeeModelList.Select(EmployeeMap.ToEntity).ToList();
            return employeeEntityList;
        }

        public async Task<EmployeeEntity> Obtain(string email)
        {
            var employeeModel = await employeesDatabase.Obtain(email);
            var employeeEntity = employeeModel.ToEntity();
            return employeeEntity;
        }

        public async Task<bool> Insert(EmployeeEntity employee)
        {
            var employeeModel = employee.ToModel();
            var rowsAffected = await employeesDatabase.Insert(employeeModel);
            return rowsAffected != 0;
        }

        public async Task<bool> Update(EmployeeEntity employee)
        {
            var employeeModel = employee.ToModel();
            var rowsAffected = await employeesDatabase.Update(employeeModel);
            return rowsAffected != 0;
        }

        public async Task<bool> Delete(string email)
        {
            var rowsAffected = await employeesDatabase.Delete(email);
            return rowsAffected != 0;
        }
    }
}