using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
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

    public async Task<IEnumerable<Meeting>> ObtainAllAsync()
    {
        var meetings = await meetingDbSet.ToListAsync();
        return meetings;
    }

    public async Task<Meeting> ObtainByIdAsync(Guid meetingId)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        return meeting ?? throw new NotFoundException("Not found");
    }

    public async Task<Guid> InsertAsync(MeetingInsert meetingInput)
    {
        var meeting = new Meeting(meetingInput.LeaderId!.Value, meetingInput.LedId!.Value, meetingInput.MeetingDate!.Value, meetingInput.Annotation!);
        await meetingDbSet.AddAsync(meeting);
        await dbContext.SaveChangesAsync();
        return meeting.Id;
    }

    public async Task<Meeting> UpdateAsync(Guid meetingId, MeetingUpdate meetingInput)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        if (meeting is null) throw new NotFoundException("Not found");
        meeting.Update(meetingInput.MeetingDate!.Value, meetingInput.Annotation!);
        meetingDbSet.Update(meeting);
        await dbContext.SaveChangesAsync();
        return meeting;
    }

    public async Task DeleteAsync(Guid meetingId)
    {
        var meeting = await meetingDbSet.SingleOrDefaultAsync(employee => employee.Id == meetingId);
        if (meeting is null) throw new NotFoundException("Not found");
        meetingDbSet.Remove(meeting);
        await dbContext.SaveChangesAsync();
    }
}
