using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IHistoricalsService
    {
        Task<IList<HistoricalEntity>> Obtain();
        Task<HistoricalEntity> Obtain(string id);
        Task<IList<HistoricalEntity>> ObtainByEmployee(string id);
        Task<IList<HistoricalEntity>> ObtainByPair(string leaderId, string ledId);
        Task<HistoricalEntity> ObtainByPairLast(string leaderId, string ledId);
        Task<HistoricalEntity> ObtainByOccurrence(string leaderId, string ledId, DateTime occurrence);
        Task<HistoricalEntity> Insert(HistoricalInputEntity historicalInput);
        Task<HistoricalEntity> Update(HistoricalEntity historical);
        Task Delete(string id);
    }
}