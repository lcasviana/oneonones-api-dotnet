using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;

namespace Oneonones.Services.Contracts;

public interface IMeetingService
{
    Task<IEnumerable<Meeting>> ObtainAllAsync();
    Task<IEnumerable<Meeting>> ObtainByOneononeAsync(Guid leaderId, Guid ledId);
    Task<Meeting> ObtainByIdAsync(Guid meetingId);
    Task<Guid> InsertAsync(MeetingInsert meetingInput);
    Task<Meeting> UpdateAsync(Guid meetingId, MeetingUpdate meetingInput);
    Task DeleteAsync(Guid meetingId);
}
