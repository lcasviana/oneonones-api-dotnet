using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IEmployeesService
    {
        Task<IList<EmployeeEntity>> ObtainAll();
        Task<(EmployeeEntity, EmployeeEntity)> ObtainPair(string leaderEmail, string ledEmail);
        Task<EmployeeEntity> Obtain(string email);
        Task Insert(EmployeeEntity employee);
        Task Update(EmployeeEntity employee);
        Task Delete(string email);
    }
}