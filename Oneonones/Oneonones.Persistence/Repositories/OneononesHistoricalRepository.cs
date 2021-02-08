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

        public async Task<IList<OneononeHistoricalEntity>> Obtain(string leaderEmail, string ledEmail)
        {
            var oneononeHistoricalModel = await oneononesHistoricalDatabase.Obtain(leaderEmail, ledEmail);
            var oneononeHistoricalEntity = oneononeHistoricalModel.Select(model => model.ToEntity());
            return oneononeHistoricalEntity.ToList();
        }

        public async Task Insert(OneononeHistoricalEntity oneonone)
        {
            var oneononeHistoricalModel = oneonone.ToModel();
            await oneononesHistoricalDatabase.Insert(oneononeHistoricalModel);
        }

        public async Task Update(OneononeHistoricalEntity oneonone)
        {
            var oneononeHistoricalModel = oneonone.ToModel();
            await oneononesHistoricalDatabase.Update(oneononeHistoricalModel);
        }

        public async Task Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            await oneononesHistoricalDatabase.Delete(leaderEmail, ledEmail, occurrence);
        }
    }
}