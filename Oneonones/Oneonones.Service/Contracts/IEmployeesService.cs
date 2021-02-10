using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IEmployeesService
    {
        Task<(EmployeeEntity, EmployeeEntity)> ObtainPair(string leaderEmail, string ledEmail);
        Task<EmployeeEntity> Obtain(string email);
        Task Insert(EmployeeEntity employee);
        Task Update(EmployeeEntity employee);
        Task Delete(string email);
    }
}