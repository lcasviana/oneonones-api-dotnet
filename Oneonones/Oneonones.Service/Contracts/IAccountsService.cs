using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IAccountsService
    {
        Task<UserEntity> Login(string email, string password);
        Task<AccountEntity> Obtain(string employeeId);
        Task<AccountEntity> Insert(AccountEntity account);
        Task<AccountEntity> Update(AccountEntity account);
        Task Delete(string employeeId);
    }
}