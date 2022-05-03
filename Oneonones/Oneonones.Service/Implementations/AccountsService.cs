using System.Net;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;

namespace Oneonones.Service.Implementations
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository accountsRepository;
        private readonly IEmployeesService employeesService;

        public AccountsService(
            IAccountsRepository accountsRepository,
            IEmployeesService employeesService)
        {
            this.accountsRepository = accountsRepository;
            this.employeesService = employeesService;
        }

        public async Task<UserEntity> Login(string email, string password)
        {
            var employee = await employeesService.ObtainByEmail(email);
            if (string.IsNullOrWhiteSpace(password))
                throw new ApiException(HttpStatusCode.BadRequest, AccountsMessages.InvalidPassword);

            var obtained = await accountsRepository.Obtain(employee.Id);
            if (obtained == null)
                throw new ApiException(HttpStatusCode.NotFound, AccountsMessages.NotFoundAccount);

            if (obtained.Password != password)
                throw new ApiException(HttpStatusCode.Unauthorized, AccountsMessages.InvalidPassword);

            var user = new UserEntity
            {
                Id = employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                Admin = obtained.Admin,
            };
            return user;
        }

        public async Task<AccountEntity> Obtain(string employeeId)
        {
            _ = await employeesService.Obtain(employeeId);

            var obtained = await accountsRepository.Obtain(employeeId);
            if (obtained == null)
                throw new ApiException(HttpStatusCode.NotFound, AccountsMessages.NotFound(employeeId));

            return obtained;
        }

        public async Task<AccountEntity> Insert(AccountEntity account)
        {
            _ = await employeesService.Obtain(account.EmployeeId);
            if (string.IsNullOrWhiteSpace(account.Password))
                throw new ApiException(HttpStatusCode.BadRequest, AccountsMessages.InvalidPassword);

            var obtained = await accountsRepository.Obtain(account.EmployeeId);
            if (obtained != null)
                throw new ApiException(HttpStatusCode.NotFound, AccountsMessages.Conflict(account.EmployeeId));

            var inserted = await accountsRepository.Insert(account);
            if (!inserted)
                throw new ApiException(HttpStatusCode.InternalServerError, AccountsMessages.Insert(account.EmployeeId));

            obtained = await accountsRepository.Obtain(account.EmployeeId);
            return obtained;
        }

        public async Task<AccountEntity> Update(AccountEntity account)
        {
            _ = await employeesService.Obtain(account.EmployeeId);
            if (string.IsNullOrWhiteSpace(account.Password))
                throw new ApiException(HttpStatusCode.BadRequest, AccountsMessages.InvalidPassword);

            var obtained = await accountsRepository.Obtain(account.EmployeeId);
            if (obtained == null)
                throw new ApiException(HttpStatusCode.NotFound, AccountsMessages.NotFound(account.EmployeeId));

            var updated = await accountsRepository.Update(account);
            if (!updated)
                throw new ApiException(HttpStatusCode.InternalServerError, AccountsMessages.Update(account.EmployeeId));

            obtained = await accountsRepository.Obtain(account.EmployeeId);
            return obtained;
        }

        public async Task Delete(string employeeId)
        {
            _ = await employeesService.Obtain(employeeId);

            var deleted = await accountsRepository.Delete(employeeId);
            if (!deleted)
                throw new ApiException(HttpStatusCode.InternalServerError, AccountsMessages.Delete(employeeId));
        }
    }
}