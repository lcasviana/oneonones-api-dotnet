using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;

namespace Oneonones.Services.Contracts;

public interface IMeetingService
{
    Task<IEnumerable<MeetingOutput>> ObtainAllAsync();
    Task<IEnumerable<MeetingOutput>> ObtainByEmployeeAsync(Guid employeeId);
    Task<IEnumerable<MeetingOutput>> ObtainByPairAsync(Guid leaderId, Guid ledId);
    Task<MeetingOutput> ObtainByPairLastAsync(Guid leaderId, Guid ledId);
    Task<MeetingOutput> ObtainByDateAsync(Guid leaderId, Guid ledId, DateTime occurrence);
    Task<MeetingOutput> ObtainByIdAsync(Guid meetingId);
    Task<Guid> InsertAsync(MeetingInput meetingInput);
    Task<MeetingOutput> UpdateAsync(Guid meetingId, MeetingInput meetingInput);
    Task DeleteAsync(Guid meetingId);
}
