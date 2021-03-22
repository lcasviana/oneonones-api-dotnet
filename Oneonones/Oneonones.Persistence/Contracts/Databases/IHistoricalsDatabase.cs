using System.Collections.Generic;
using System.Threading.Tasks;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IHistoricalsDatabase
    {
        Task<IList<HistoricalModel>> Obtain();
        Task<HistoricalModel> Obtain(string id);
        Task<IList<HistoricalModel>> ObtainByEmployee(string id);
        Task<IList<HistoricalModel>> ObtainByPair(string leaderId, string ledId);
        Task<int> Insert(HistoricalModel historical);
        Task<int> Update(HistoricalModel historical);
        Task<int> Delete(string id);
    }
}