using OneononeHistoricals.Persistence.Mapping;
using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Repositories
{
    public class OneononesHistoricalRepository : IOneononesHistoricalRepository
    {
        private readonly IOneononesHistoricalDatabase oneononesHistoricalDatabase;

        public OneononesHistoricalRepository(IOneononesHistoricalDatabase oneononesHistoricalDatabase)
        {
            this.oneononesHistoricalDatabase = oneononesHistoricalDatabase;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainAll()
        {
            var oneononeHistoricalModelList = await oneononesHistoricalDatabase.ObtainAll();
            var oneononeHistoricalEntityList = oneononeHistoricalModelList.Select(OneononeHistoricalMap.ToEntity).ToList();
            return oneononeHistoricalEntityList;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainByEmployee(string email)
        {
            var oneononeHistoricalModelList = await oneononesHistoricalDatabase.ObtainByEmployee(email);
            var oneononeHistoricalEntityList = oneononeHistoricalModelList.Select(OneononeHistoricalMap.ToEntity).ToList();
            return oneononeHistoricalEntityList;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var oneononeHistoricalModelList = await oneononesHistoricalDatabase.ObtainByPair(leaderEmail, ledEmail);
            var oneononeHistoricalEntityList = oneononeHistoricalModelList.Select(OneononeHistoricalMap.ToEntity).ToList();
            return oneononeHistoricalEntityList;
        }

        public async Task<OneononeHistoricalEntity> ObtainByPairLast(string leaderEmail, string ledEmail)
        {
            var oneononeHistoricalModel = await oneononesHistoricalDatabase.ObtainByPairLast(leaderEmail, ledEmail);
            var oneononeHistoricalEntity = oneononeHistoricalModel.ToEntity();
            return oneononeHistoricalEntity;
        }

        public async Task<OneononeHistoricalEntity> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var oneononeHistoricalModel = await oneononesHistoricalDatabase.ObtainByPairOccurrence(leaderEmail, ledEmail, occurrence);
            var oneononeHistoricalEntity = oneononeHistoricalModel.ToEntity();
            return oneononeHistoricalEntity;
        }

        public async Task<bool> Insert(OneononeHistoricalEntity oneonone)
        {
            var oneononeHistoricalModel = oneonone.ToModel();
            var rowsAffected = await oneononesHistoricalDatabase.Insert(oneononeHistoricalModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(OneononeHistoricalEntity oneonone)
        {
            var oneononeHistoricalModel = oneonone.ToModel();
            var rowsAffected = await oneononesHistoricalDatabase.Update(oneononeHistoricalModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var rowsAffected = await oneononesHistoricalDatabase.Delete(leaderEmail, ledEmail, occurrence);
            return rowsAffected == 1;
        }
    }
}