using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IEmployeesRepository
    {
        Task<EmployeeEntity> Obtain(string email);
        Task Insert(EmployeeEntity employee);
        Task Update(EmployeeEntity employee);
        Task Delete(string email);
    }
}