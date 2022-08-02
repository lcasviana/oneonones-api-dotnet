using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
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

    public async Task<IEnumerable<OneononeOutput>> ObtainAllAsync()
    {
        var oneonones = await oneononeDbSet.ToListAsync();
        return oneonones.Select(oneonone => (OneononeOutput)oneonone);
    }

    public async Task<IEnumerable<OneononeOutput>> ObtainByEmployeeAsync(Guid employeeId)
    {
        var oneonones = await oneononeDbSet
            .Where(oneonone => oneonone.LeaderId == employeeId || oneonone.LedId == employeeId)
            .ToListAsync();
        return oneonones.Select(oneonone => (OneononeOutput)oneonone);
    }

    public async Task<OneononeOutput> ObtainByPairAsync(Guid leaderId, Guid ledId)
    {
        var oneonone = await oneononeDbSet
            .SingleOrDefaultAsync(oneonone => oneonone.LeaderId == leaderId && oneonone.LedId == ledId);
        return oneonone is null
            ? throw new NotFoundException()
            : (OneononeOutput)oneonone;
    }

    public async Task<OneononeOutput> ObtainByIdAsync(Guid oneononeId)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        return oneonone is null
            ? throw new NotFoundException()
            : (OneononeOutput)oneonone;
    }

    public async Task<Guid> InsertAsync(OneononeInput oneononeInput)
    {
        var oneonone = new Oneonone
        {
            LeaderId = oneononeInput.LeaderId!.Value,
            LedId = oneononeInput.LedId!.Value,
            Frequency = oneononeInput.Frequency!.Value,
        };

        await oneononeDbSet.AddAsync(oneonone);
        await dbContext.SaveChangesAsync();
        return oneonone.Id;
    }

    public async Task<OneononeOutput> UpdateAsync(Guid oneononeId, OneononeInput oneononeInput)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        if (oneonone is null) throw new NotFoundException();

        oneonone.Frequency = oneononeInput.Frequency!.Value;
        oneononeDbSet.Update(oneonone);
        await dbContext.SaveChangesAsync();
        return (OneononeOutput)oneonone;
    }

    public async Task DeleteAsync(Guid oneononeId)
    {
        var oneonone = await oneononeDbSet.SingleOrDefaultAsync(oneonone => oneonone.Id == oneononeId);
        if (oneonone is null) throw new NotFoundException();

        oneononeDbSet.Remove(oneonone);
        await dbContext.SaveChangesAsync();
    }
}
