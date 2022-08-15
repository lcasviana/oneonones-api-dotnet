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

    private IQueryable<Meeting> MeetingQuery => meetingDbSet
        .Include(meeting => meeting.Leader)
        .Include(meeting => meeting.Led);

    public async Task<IEnumerable<Meeting>> ObtainAllAsync()
    {
        var meetings = await MeetingQuery.ToListAsync();
        return meetings;
    }

    public async Task<IEnumerable<Meeting>> ObtainByOneononeAsync(Guid leaderId, Guid ledId)
    {
        var meetings = await MeetingQuery
            .Where(meeting => meeting.LeaderId == leaderId && meeting.LedId == ledId)
            .ToListAsync();
        return meetings;
    }

    public async Task<Meeting> ObtainByIdAsync(Guid meetingId)
    {
        var meeting = await MeetingQuery.SingleOrDefaultAsync(meeting => meeting.Id == meetingId);
        return meeting ?? throw new NotFoundException(NotFoundEntity.Meeting);
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
        var meeting = await ObtainByIdAsync(meetingId);
        meeting.Update(meetingInput.MeetingDate!.Value, meetingInput.Annotation!);
        meetingDbSet.Update(meeting);
        await dbContext.SaveChangesAsync();
        return meeting;
    }

    public async Task DeleteAsync(Guid meetingId)
    {
        var meeting = await ObtainByIdAsync(meetingId);
        meetingDbSet.Remove(meeting);
        await dbContext.SaveChangesAsync();
    }
}
