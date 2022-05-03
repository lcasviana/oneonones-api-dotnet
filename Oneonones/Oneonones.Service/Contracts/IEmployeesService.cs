using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IEmployeesService
    {
        Task<IList<EmployeeEntity>> Obtain();
        Task<EmployeeEntity> Obtain(string id);
        Task<EmployeeEntity> ObtainByEmail(string email);
        Task<(EmployeeEntity, EmployeeEntity)> ObtainPair(string leaderId, string ledId);
        Task<EmployeeEntity> Insert(EmployeeInputEntity employeeInput);
        Task<EmployeeEntity> Update(EmployeeEntity employee);
        Task Delete(string id);
    }
}