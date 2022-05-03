using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;

namespace Oneonones.Persistence.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IAccountsDatabase accountsDatabase;

        public AccountsRepository(IAccountsDatabase accountsDatabase)
        {
            this.accountsDatabase = accountsDatabase;
        }

        public async Task<AccountEntity> Obtain(string employeeId)
        {
            var model = await accountsDatabase.Obtain(employeeId);
            var entity = model.ToEntity();
            return entity;
        }

        public async Task<bool> Insert(AccountEntity account)
        {
            var model = account.ToModel();
            var rowsAffected = await accountsDatabase.Insert(model);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(AccountEntity account)
        {
            var model = account.ToModel();
            var rowsAffected = await accountsDatabase.Update(model);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string employeeId)
        {
            var rowsAffected = await accountsDatabase.Delete(employeeId);
            return rowsAffected == 1;
        }
    }
}