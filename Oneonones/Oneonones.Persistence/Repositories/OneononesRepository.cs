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

        public async Task<bool> Insert(OneononeEntity oneonone)
        {
            var oneononeModel = oneonone.ToModel();
            var rowsAffected = await oneononesDatabase.Insert(oneononeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(OneononeEntity oneonone)
        {
            var oneononeModel = oneonone.ToModel();
            var rowsAffected = await oneononesDatabase.Update(oneononeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string leaderEmail, string ledEmail)
        {
            var rowsAffected = await oneononesDatabase.Delete(leaderEmail, ledEmail);
            return rowsAffected == 1;
        }
    }
}