using System.Threading.Tasks;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IAccountsDatabase
    {
        Task<AccountModel> Obtain(string employeeId);
        Task<int> Insert(AccountModel account);
        Task<int> Update(AccountModel account);
        Task<int> Delete(string employeeId);
    }
}