using Oneonones.Domain.Entities;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IHistoricalsRepository
    {
        Task<IList<HistoricalEntity>> Obtain();
        Task<HistoricalEntity> Obtain(string id);
        Task<IList<HistoricalEntity>> ObtainByEmployee(string id);
        Task<IList<HistoricalEntity>> ObtainByPair(string leaderId, string ledId);
        Task<bool> Insert(HistoricalEntity historicalEntity);
        Task<bool> Update(HistoricalEntity historicalEntity);
        Task<bool> Delete(string id);
    }
}