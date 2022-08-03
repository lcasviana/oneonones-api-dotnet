using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;

namespace Oneonones.Services.Contracts;

public interface IOneononeService
{
    Task<IEnumerable<Oneonone>> ObtainAllAsync();
    Task<Oneonone> ObtainByIdAsync(Guid oneononeId);
    Task<Guid> InsertAsync(OneononeInsert oneononeInput);
    Task<Oneonone> UpdateAsync(Guid oneononeId, OneononeUpdate oneononeInput);
    Task DeleteAsync(Guid oneononeId);
}
