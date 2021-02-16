using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IList<EmployeeEntity>> ObtainAll();
        Task<EmployeeEntity> Obtain(string email);
        Task Insert(EmployeeEntity employee);
        Task Update(EmployeeEntity employee);
        Task Delete(string email);
    }
}