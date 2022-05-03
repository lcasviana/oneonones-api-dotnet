using System.Net;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;

namespace Oneonones.Service.Implementations
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        public async Task<IList<EmployeeEntity>> Obtain()
        {
            var employeeList = await employeesRepository.Obtain();
            return employeeList;
        }

        public async Task<EmployeeEntity> Obtain(string id)
        {
            if (!Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var employee = await employeesRepository.Obtain(id);
            if (employee == null)
                throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundId(id));

            return employee;
        }

        public async Task<EmployeeEntity> ObtainByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);

            var employee = await employeesRepository.ObtainByEmail(email);
            if (employee == null)
                throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundEmail(email));

            return employee;
        }

        public async Task<(EmployeeEntity, EmployeeEntity)> ObtainPair(string leaderId, string ledId)
        {
            var requestErrors = new string[]
            {
                Guid.TryParse(leaderId, out var _) ? null : GlobalMessages.InvalidId(leaderId),
                Guid.TryParse(ledId, out var _) ? null : GlobalMessages.InvalidId(ledId),
                leaderId == ledId ? EmployeesMessages.Same : null,
            }.Where(e => e != null);
            if (requestErrors.Any())
                throw new ApiException(HttpStatusCode.BadRequest, requestErrors.ToList());

            var leaderTask = employeesRepository.Obtain(leaderId);
            var ledTask = employeesRepository.Obtain(ledId);

            var leaderEntity = await leaderTask;
            var ledEntity = await ledTask;

            var taskErrors = new string[]
            {
                leaderEntity != null ? null : EmployeesMessages.NotFoundLeader(leaderId),
                ledEntity != null ? null : EmployeesMessages.NotFoundLed(ledId),
            }.Where(e => e != null);
            if (taskErrors.Any())
                throw new ApiException(HttpStatusCode.NotFound, taskErrors.ToList());

            return (leaderEntity, ledEntity);
        }

        public async Task<EmployeeEntity> Insert(EmployeeInputEntity employeeInput)
        {
            var requestErrors = new string[]
            {
                string.IsNullOrWhiteSpace(employeeInput.Email) ? EmployeesMessages.InvalidEmail : null,
                string.IsNullOrWhiteSpace(employeeInput.Name) ? EmployeesMessages.InvalidName : null,
            }.Where(e => e != null);
            if (requestErrors.Any())
                throw new ApiException(HttpStatusCode.BadRequest, requestErrors.ToList());

            var employeeObtained = await employeesRepository.ObtainByEmail(employeeInput.Email);
            if (employeeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, EmployeesMessages.Conflict(employeeInput.Email));

            var employee = new EmployeeEntity(employeeInput.Email, employeeInput.Name);
            var inserted = await employeesRepository.Insert(employee);
            if (!inserted)
                throw new ApiException(HttpStatusCode.InternalServerError, EmployeesMessages.Insert(employee.Email));

            return employee;
        }

        public async Task<EmployeeEntity> Update(EmployeeEntity employee)
        {
            var requestErrors = new string[]
            {
                Guid.TryParse(employee.Id, out var _) ? null : GlobalMessages.InvalidId(employee.Id),
                string.IsNullOrWhiteSpace(employee.Email) ? EmployeesMessages.InvalidEmail : null,
                string.IsNullOrWhiteSpace(employee.Name) ? EmployeesMessages.InvalidName : null,
            }.Where(e => e != null);
            if (requestErrors.Any())
                throw new ApiException(HttpStatusCode.BadRequest, requestErrors.ToList());

            var employeeObtained = await employeesRepository.Obtain(employee.Id);
            if (employeeObtained == null)
                throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundId(employee.Id));

            var oneononeConflict = await employeesRepository.ObtainByEmail(employee.Email);
            if (oneononeConflict != null && oneononeConflict.Id != employeeObtained.Id)
                throw new ApiException(HttpStatusCode.Conflict, EmployeesMessages.Conflict(employee.Email));

            var updated = await employeesRepository.Update(employee);
            if (!updated)
                throw new ApiException(HttpStatusCode.InternalServerError, EmployeesMessages.Update(employee.Email));

            return employee;
        }

        public async Task Delete(string id)
        {
            if (!Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var employee = await employeesRepository.Obtain(id);
            if (employee == null)
                throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundId(id));

            var deleted = await employeesRepository.Delete(id);
            if (!deleted)
                throw new ApiException(HttpStatusCode.InternalServerError, EmployeesMessages.Delete(id));
        }
    }
}