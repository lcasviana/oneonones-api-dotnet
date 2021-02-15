using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Mapping;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Repositories
{
    public class OneononesRepository : IOneononesRepository
    {
        private readonly IOneononesDatabase oneononesDatabase;

        public OneononesRepository(IOneononesDatabase oneoninesDatabase)
        {
            this.oneononesDatabase = oneoninesDatabase;
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