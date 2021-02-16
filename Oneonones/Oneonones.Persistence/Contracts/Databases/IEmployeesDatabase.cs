using Oneonones.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IEmployeesDatabase
    {
        Task<IList<EmployeeModel>> ObtainAll();
        Task<EmployeeModel> Obtain(string email);
        Task Insert(EmployeeModel employee);
        Task Update(EmployeeModel employee);
        Task Delete(string email);
    }
}