using Oneonones.Domain.Entities;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IList<EmployeeEntity>> Obtain();
        Task<EmployeeEntity> Obtain(string id);
        Task<EmployeeEntity> ObtainByEmail(string email);
        Task<bool> Insert(EmployeeEntity employeeEntity);
        Task<bool> Update(EmployeeEntity employeeEntity);
        Task<bool> Delete(string id);
    }
}