using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;

namespace Oneonones.Persistence.Repositories
{
    public class HistoricalsRepository : IHistoricalsRepository
    {
        private readonly IHistoricalsDatabase historicalsDatabase;

        public HistoricalsRepository(IHistoricalsDatabase historicalsDatabase)
        {
            this.historicalsDatabase = historicalsDatabase;
        }

        public async Task<IList<HistoricalEntity>> Obtain()
        {
            var historicalModelList = await historicalsDatabase.Obtain();
            var historicalEntityList = historicalModelList.Select(HistoricalMap.ToEntity).ToList();
            return historicalEntityList;
        }

        public async Task<HistoricalEntity> Obtain(string id)
        {
            var historicalModel = await historicalsDatabase.Obtain(id);
            var historicalEntity = historicalModel.ToEntity();
            return historicalEntity;
        }

        public async Task<IList<HistoricalEntity>> ObtainByEmployee(string id)
        {
            var historicalModelList = await historicalsDatabase.ObtainByEmployee(id);
            var historicalEntityList = historicalModelList.Select(HistoricalMap.ToEntity).ToList();
            return historicalEntityList;
        }

        public async Task<IList<HistoricalEntity>> ObtainByPair(string leaderId, string ledId)
        {
            var historicalModelList = await historicalsDatabase.ObtainByPair(leaderId, ledId);
            var historicalEntityList = historicalModelList.Select(HistoricalMap.ToEntity).ToList();
            return historicalEntityList;
        }

        public async Task<bool> Insert(HistoricalEntity historicalEntity)
        {
            var historicalModel = historicalEntity.ToModel();
            var rowsAffected = await historicalsDatabase.Insert(historicalModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(HistoricalEntity historicalEntity)
        {
            var historicalModel = historicalEntity.ToModel();
            var rowsAffected = await historicalsDatabase.Update(historicalModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string id)
        {
            var rowsAffected = await historicalsDatabase.Delete(id);
            return rowsAffected == 1;
        }
    }
}
