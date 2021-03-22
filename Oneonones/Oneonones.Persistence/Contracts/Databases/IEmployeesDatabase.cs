using Oneonones.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IEmployeesDatabase
    {
        Task<IList<EmployeeModel>> Obtain();
        Task<EmployeeModel> Obtain(string id);
        Task<EmployeeModel> ObtainByEmail(string email);
        Task<int> Insert(EmployeeModel employee);
        Task<int> Update(EmployeeModel employee);
        Task<int> Delete(string id);
    }
}