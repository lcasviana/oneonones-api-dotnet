using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;

namespace Oneonones.Services.Contracts;

public interface IOneononeService
{
    Task<IEnumerable<OneononeOutput>> ObtainAllAsync();
    Task<IEnumerable<OneononeOutput>> ObtainByEmployeeAsync(Guid employeeId);
    Task<OneononeOutput> ObtainByPairAsync(Guid leaderId, Guid ledId);
    Task<OneononeOutput> ObtainByIdAsync(Guid oneononeId);
    Task<Guid> InsertAsync(OneononeInput oneononeInput);
    Task<OneononeOutput> UpdateAsync(Guid oneononeId, OneononeInput oneononeInput);
    Task DeleteAsync(Guid oneononeId);
}
