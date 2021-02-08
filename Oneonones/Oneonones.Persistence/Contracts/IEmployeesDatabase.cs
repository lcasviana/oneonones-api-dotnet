using Oneonones.Persistence.Models;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts
{
    public interface IEmployeesDatabase
    {
        Task<EmployeeModel> Obtain(string email);
        Task Insert(EmployeeModel employee);
        Task Update(EmployeeModel employee);
        Task Delete(string email);
    }
}