using Oneonones.Domain.Entities;
using Oneonones.Service.Contracts;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class EmployeesService : IEmployeesService
    {
        public async Task<EmployeeEntity> Obtain(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task Insert(EmployeeEntity employee)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(EmployeeEntity employee)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}