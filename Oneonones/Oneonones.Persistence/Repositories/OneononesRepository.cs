using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;

namespace Oneonones.Persistence.Repositories
{
    public class OneononesRepository : IOneononesRepository
    {
        private readonly IOneononesDatabase oneononesDatabase;

        public OneononesRepository(IOneononesDatabase oneononesDatabase)
        {
            this.oneononesDatabase = oneononesDatabase;
        }

        public async Task<IList<OneononeEntity>> Obtain()
        {
            var oneononeModelList = await oneononesDatabase.Obtain();
            var oneononeEntityList = oneononeModelList.Select(OneononeMap.ToEntity).ToList();
            return oneononeEntityList;
        }

        public async Task<OneononeEntity> Obtain(string id)
        {
            var oneononeModel = await oneononesDatabase.Obtain(id);
            var oneononeEntity = oneononeModel.ToEntity();
            return oneononeEntity;
        }

        public async Task<IList<OneononeEntity>> ObtainByEmployee(string id)
        {
            var oneononeModelList = await oneononesDatabase.ObtainByEmployee(id);
            var oneononeEntityList = oneononeModelList.Select(OneononeMap.ToEntity).ToList();
            return oneononeEntityList;
        }

        public async Task<OneononeEntity> ObtainByPair(string leaderId, string ledId)
        {
            var oneononeModel = await oneononesDatabase.ObtainByPair(leaderId, ledId);
            var oneononeEntity = oneononeModel.ToEntity();
            return oneononeEntity;
        }

        public async Task<bool> Insert(OneononeEntity oneononeEntity)
        {
            var oneononeModel = oneononeEntity.ToModel();
            var rowsAffected = await oneononesDatabase.Insert(oneononeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Update(OneononeEntity oneononeEntity)
        {
            var oneononeModel = oneononeEntity.ToModel();
            var rowsAffected = await oneononesDatabase.Update(oneononeModel);
            return rowsAffected == 1;
        }

        public async Task<bool> Delete(string id)
        {
            var rowsAffected = await oneononesDatabase.Delete(id);
            return rowsAffected == 1;
        }
    }
}