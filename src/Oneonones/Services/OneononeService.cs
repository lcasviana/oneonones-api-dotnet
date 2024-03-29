using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
using Oneonones.Repositories.Context;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Services;

public class OneononeService : IOneononeService
{
    private readonly OneononeContext dbContext;
    private readonly DbSet<Oneonone> oneononeDbSet;

    public OneononeService(OneononeContext context)
    {
        dbContext = context;
        oneononeDbSet = context.Oneonone;
    }

    private IQueryable<Oneonone> OneononeQuery => oneononeDbSet
        .Include(oneonone => oneonone.Leader)
        .Include(oneonone => oneonone.Led);

    public async Task<IEnumerable<Oneonone>> ObtainAllAsync()
    {
        var oneonones = await OneononeQuery.ToListAsync();
        return oneonones;
    }

    public async Task<IEnumerable<Oneonone>> ObtainByEmployeeAsync(Guid employeeId)
    {
        var oneonones = await OneononeQuery
            .Where(oneonone => oneonone.LeaderId == employeeId || oneonone.LedId == employeeId)
            .ToListAsync();
        return oneonones;
    }

    public async Task<Oneonone> ObtainByIdAsync(Guid oneononeId)
    {
        var oneonone = await OneononeQuery.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        return oneonone ?? throw new NotFoundException(NotFoundEntity.Oneonone);
    }

    public async Task<Guid> InsertAsync(OneononeInsert oneononeInput)
    {
        var oneonone = new Oneonone(oneononeInput.LeaderId!.Value, oneononeInput.LedId!.Value, oneononeInput.Frequency!.Value);
        await oneononeDbSet.AddAsync(oneonone);
        await dbContext.SaveChangesAsync();
        return oneonone.Id;
    }

    public async Task<Oneonone> UpdateAsync(Guid oneononeId, OneononeUpdate oneononeInput)
    {
        var oneonone = await ObtainByIdAsync(oneononeId);
        oneonone.Update(oneononeInput.Frequency!.Value);
        oneononeDbSet.Update(oneonone);
        await dbContext.SaveChangesAsync();
        return oneonone;
    }

    public async Task DeleteAsync(Guid oneononeId)
    {
        var oneonone = await ObtainByIdAsync(oneononeId);
        oneononeDbSet.Remove(oneonone);
        await dbContext.SaveChangesAsync();
    }
}
