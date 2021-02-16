using Oneonones.Domain.Entities;
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
            if (string.IsNullOrWhiteSpace(leaderEmail)) throw new ApiException(HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(ledEmail)) throw new ApiException(HttpStatusCode.BadRequest);

            var leaderTask = employeesRepository.Obtain(leaderEmail);
            var ledTask = employeesRepository.Obtain(ledEmail);

            var leader = (await leaderTask) ?? throw new ApiException(HttpStatusCode.NotFound);
            var led = (await ledTask) ?? throw new ApiException(HttpStatusCode.NotFound);

            return (leader, led);
        }

        public async Task<EmployeeEntity> Obtain(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ApiException(HttpStatusCode.BadRequest);

            var employee = (await employeesRepository.Obtain(email))
                ?? throw new ApiException(HttpStatusCode.NotFound);

            return employee;
        }

        public async Task Insert(EmployeeEntity employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ApiException(HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(employee.Name)) throw new ApiException(HttpStatusCode.BadRequest);

            var employeeObtained = await employeesRepository.Obtain(employee.Email);
            if (employeeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict);

            await employeesRepository.Insert(employee);
        }

        public async Task Update(EmployeeEntity employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Email)) throw new ApiException(HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(employee.Name)) throw new ApiException(HttpStatusCode.BadRequest);

            _ = (await employeesRepository.Obtain(employee.Email))
                ?? throw new ApiException(HttpStatusCode.NotFound);

            await employeesRepository.Update(employee);
        }

        public async Task Delete(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ApiException(HttpStatusCode.BadRequest);

            await employeesRepository.Delete(email);
        }
    }
}