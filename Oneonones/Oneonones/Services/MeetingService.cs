using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
using Oneonones.Repositories.Context;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Services;

public class MeetingService : IMeetingService
{
    private readonly OneononeContext dbContext;
    private readonly DbSet<Meeting> meetingDbSet;

    public MeetingService(OneononeContext context)
    {
        dbContext = context;
        meetingDbSet = context.Meeting;
    }

    public async Task<IEnumerable<MeetingOutput>> ObtainAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MeetingOutput>> ObtainByEmployeeAsync(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MeetingOutput>> ObtainByPairAsync(Guid leaderId, Guid ledId)
    {
        throw new NotImplementedException();
    }

    public async Task<MeetingOutput> ObtainByPairLastAsync(Guid leaderId, Guid ledId)
    {
        throw new NotImplementedException();
    }

    public async Task<MeetingOutput> ObtainByDateAsync(Guid leaderId, Guid ledId, DateTime occurrence)
    {
        throw new NotImplementedException();
    }

    public async Task<MeetingOutput> ObtainByIdAsync(Guid meetingId)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        return meeting is null
            ? throw new NotFoundException()
            : (MeetingOutput)meeting;
    }

    public async Task<Guid> InsertAsync(MeetingInput meetingInput)
    {
        var meeting = new Meeting
        {
            LeaderId = meetingInput.LeaderId!.Value,
            LedId = meetingInput.LedId!.Value,
            MeetingDate = meetingInput.MeetingDate!.Value,
            Annotation = meetingInput.Annotation!,
        };

        await meetingDbSet.AddAsync(meeting);
        await dbContext.SaveChangesAsync();
        return meeting.Id;
    }

    public async Task<MeetingOutput> UpdateAsync(Guid meetingId, MeetingInput meetingInput)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        if (meeting is null) throw new NotFoundException();

        meeting.MeetingDate = meetingInput.MeetingDate!.Value;
        meeting.Annotation = meetingInput.Annotation!;
        meetingDbSet.Update(meeting);
        await dbContext.SaveChangesAsync();
        return (MeetingOutput)meeting;
    }

    public async Task DeleteAsync(Guid meetingId)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        if (meeting is null) throw new NotFoundException();

        meetingDbSet.Remove(meeting);
        await dbContext.SaveChangesAsync();
    }
}
