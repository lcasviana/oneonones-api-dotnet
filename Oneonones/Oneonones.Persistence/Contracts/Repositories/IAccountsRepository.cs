using System.Threading.Tasks;
using Oneonones.Domain.Entities;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IAccountsRepository
    {
        Task<AccountEntity> Obtain(string employeeId);
        Task<bool> Insert(AccountEntity account);
        Task<bool> Update(AccountEntity account);
        Task<bool> Delete(string employeeId);
    }
}