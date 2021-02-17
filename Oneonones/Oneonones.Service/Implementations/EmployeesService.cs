using Oneonones.Domain.Entities;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        public async Task<IList<EmployeeEntity>> ObtainAll()
        {
            var employeeList = await employeesRepository.ObtainAll();
            return employeeList;
        }

        public async Task<(EmployeeEntity, EmployeeEntity)> ObtainPair(string leaderEmail, string ledEmail)
        {
            if (string.IsNullOrWhiteSpace(leaderEmail)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmailLeader);
            if (string.IsNullOrWhiteSpace(ledEmail)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmailLed);

            var leaderTask = employeesRepository.Obtain(leaderEmail);
            var ledTask = employeesRepository.Obtain(ledEmail);

            var leader = (await leaderTask) ?? throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundLeader, leaderEmail);
            var led = (await ledTask) ?? throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFoundLed, ledEmail);

            return (leader, led);
        }

        public async Task<EmployeeEntity> Obtain(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);

            var employee = (await employeesRepository.Obtain(email))
                ?? throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFound, email);

            return employee;
        }

        public async Task Insert(EmployeeEntity employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);
            if (string.IsNullOrWhiteSpace(employee.Name)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidName);

            var employeeObtained = await employeesRepository.Obtain(employee.Email);
            if (employeeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, EmployeesMessages.Conflict, employee.Email);

            var inserted = await employeesRepository.Insert(employee);
            if (!inserted) throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Insert, employee.Email);
        }

        public async Task Update(EmployeeEntity employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);
            if (string.IsNullOrWhiteSpace(employee.Name)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidName);

            _ = (await employeesRepository.Obtain(employee.Email))
                ?? throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFound, employee.Email);

            var updated = await employeesRepository.Update(employee);
            if (!updated) throw new ApiException(HttpStatusCode.InternalServerError, EmployeesMessages.Update, employee.Email);
        }

        public async Task Delete(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);

            _ = (await employeesRepository.Obtain(email))
                ?? throw new ApiException(HttpStatusCode.NotFound, EmployeesMessages.NotFound, email);

            var deleted = await employeesRepository.Delete(email);
            if (!deleted) throw new ApiException(HttpStatusCode.InternalServerError, EmployeesMessages.Delete, email);
        }
    }
}