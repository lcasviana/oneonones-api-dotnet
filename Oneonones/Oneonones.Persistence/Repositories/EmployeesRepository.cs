using Employees.Persistence.Mapping;
using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
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

        public async Task<EmployeeEntity> Obtain(string email)
        {
            var employeeModel = await employeesDatabase.Obtain(email);
            var employeeEntity = employeeModel.ToEntity();
            return employeeEntity;
        }

        public async Task Insert(EmployeeEntity employee)
        {
            var employeeModel = employee.ToModel();
            await employeesDatabase.Insert(employeeModel);
        }

        public async Task Update(EmployeeEntity employee)
        {
            var employeeModel = employee.ToModel();
            await employeesDatabase.Update(employeeModel);
        }

        public async Task Delete(string email)
        {
            await employeesDatabase.Delete(email);
        }
    }
}