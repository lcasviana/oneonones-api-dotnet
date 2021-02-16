using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Repositories
{
    public class OneononesRepository : IOneononesRepository
    {
        private readonly IOneononesDatabase oneononesDatabase;

        public OneononesRepository(IOneononesDatabase oneononesDatabase)
        {
            this.oneononesDatabase = oneononesDatabase;
        }

        public async Task<IList<OneononeEntity>> ObtainAll()
        {
            var oneononeModelList = await oneononesDatabase.ObtainAll();
            var oneononeEntityList = oneononeModelList.Select(OneononeMap.ToEntity).ToList();
            return oneononeEntityList;
        }

        public async Task<IList<OneononeEntity>> ObtainByEmployee(string email)
        {
            var oneononeModelList = await oneononesDatabase.ObtainByEmployee(email);
            var oneononeEntityList = oneononeModelList.Select(OneononeMap.ToEntity).ToList();
            return oneononeEntityList;
        }

        public async Task<OneononeEntity> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var oneononeModel = await oneononesDatabase.ObtainByPair(leaderEmail, ledEmail);
            var oneononeEntity = oneononeModel.ToEntity();
            return oneononeEntity;
        }

        public async Task Insert(OneononeEntity oneonone)
        {
            var oneononeModel = oneonone.ToModel();
            await oneononesDatabase.Insert(oneononeModel);
        }

        public async Task Update(OneononeEntity oneonone)
        {
            var oneononeModel = oneonone.ToModel();
            await oneononesDatabase.Update(oneononeModel);
        }

        public async Task Delete(string leaderEmail, string ledEmail)
        {
            await oneononesDatabase.Delete(leaderEmail, ledEmail);
        }
    }
}