using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;

namespace Oneonones.Persistence.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IEmployeesDatabase employeesDatabase;

        public EmployeesRepository(IEmployeesDatabase employeesDatabase)
        {
            this.employeesDatabase = employeesDatabase;
        }

        public async Task<IList<EmployeeEntity>> Obtain()
        {
            var employeeModelList = await employeesDatabase.Obtain();
            var employeeEntityList = employeeModelList.Select(EmployeeMap.ToEntity).ToList();
            return employeeEntityList;
        }

        public async Task<EmployeeEntity> Obtain(string id)
        {
            var employeeModel = await employeesDatabase.Obtain(id);
            var employeeEntity = employeeModel.ToEntity();
            return employeeEntity;
        }

        public async Task<EmployeeEntity> ObtainByEmail(string email)
        {
            var employeeModel = await employeesDatabase.ObtainByEmail(email);
            var employeeEntity = employeeModel.ToEntity();
            return employeeEntity;
        }

        public async Task<bool> Insert(EmployeeEntity employeeEntity)
        {
            var employeeModel = employeeEntity.ToModel();
            var rowsAffected = await employeesDatabase.Insert(employeeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(EmployeeEntity employeeEntity)
        {
            var employeeModel = employeeEntity.ToModel();
            var rowsAffected = await employeesDatabase.Update(employeeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string email)
        {
            var rowsAffected = await employeesDatabase.Delete(email);
            return rowsAffected == 1;
        }
    }
}