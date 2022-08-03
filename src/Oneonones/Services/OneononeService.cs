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

    public async Task<IEnumerable<Oneonone>> ObtainAllAsync()
    {
        var oneonones = await oneononeDbSet.ToListAsync();
        return oneonones;
    }

    public async Task<Oneonone> ObtainByIdAsync(Guid oneononeId)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        return oneonone ?? throw new NotFoundException("Not found");
    }

    public async Task<Guid> InsertAsync(OneononeInput oneononeInput)
    {
        var oneonone = new Oneonone(oneononeInput.LeaderId!.Value, oneononeInput.LedId!.Value, oneononeInput.Frequency!.Value);
        await oneononeDbSet.AddAsync(oneonone);
        await dbContext.SaveChangesAsync();
        return oneonone.Id;
    }

    public async Task<Oneonone> UpdateAsync(Guid oneononeId, OneononeInput oneononeInput)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        if (oneonone is null) throw new NotFoundException("Not found");
        oneonone.Update(oneononeInput.Frequency!.Value);
        oneononeDbSet.Update(oneonone);
        await dbContext.SaveChangesAsync();
        return oneonone;
    }

    public async Task DeleteAsync(Guid oneononeId)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        if (oneonone is null) throw new NotFoundException("Not found");
        oneononeDbSet.Remove(oneonone);
        await dbContext.SaveChangesAsync();
    }
}
