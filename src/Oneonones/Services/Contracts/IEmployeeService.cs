using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;

namespace Oneonones.Services.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> ObtainAllAsync();
    Task<Employee> ObtainByEmailAsync(string employeeEmail);
    Task<Employee> ObtainByIdAsync(Guid employeeId);
    Task<Guid> InsertAsync(EmployeeInput employeeInput);
    Task<Employee> UpdateAsync(Guid employeeId, EmployeeInput employeeInput);
    Task DeleteAsync(Guid employeeId);
}
