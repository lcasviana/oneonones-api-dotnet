using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;

namespace Oneonones.Services.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeOutput>> ObtainAllAsync();
    Task<EmployeeOutput> ObtainByEmailAsync(string employeeEmail);
    Task<EmployeeOutput> ObtainByIdAsync(Guid employeeId);
    Task<Guid> InsertAsync(EmployeeInput employeeInput);
    Task<EmployeeOutput> UpdateAsync(Guid employeeId, EmployeeInput employeeInput);
    Task DeleteAsync(Guid employeeId);
}
