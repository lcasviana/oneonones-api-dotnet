using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;

namespace Oneonones.Services.Contracts;

public interface IMeetingService
{
    Task<IEnumerable<Meeting>> ObtainAllAsync();
    Task<IEnumerable<Meeting>> ObtainByEmployeeAsync(Guid employeeId);
    Task<IEnumerable<Meeting>> ObtainByPairAsync(Guid leaderId, Guid ledId);
    Task<Meeting> ObtainByPairLastAsync(Guid leaderId, Guid ledId);
    Task<Meeting> ObtainByDateAsync(Guid leaderId, Guid ledId, DateTime occurrence);
    Task<Meeting> ObtainByIdAsync(Guid meetingId);
    Task<Guid> InsertAsync(MeetingInput meetingInput);
    Task<Meeting> UpdateAsync(Guid meetingId, MeetingInput meetingInput);
    Task DeleteAsync(Guid meetingId);
}
