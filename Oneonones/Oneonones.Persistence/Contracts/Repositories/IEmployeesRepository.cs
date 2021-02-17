using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IList<EmployeeEntity>> ObtainAll();
        Task<EmployeeEntity> Obtain(string email);
        Task<bool> Insert(EmployeeEntity employee);
        Task<bool> Update(EmployeeEntity employee);
        Task<bool> Delete(string email);
    }
}