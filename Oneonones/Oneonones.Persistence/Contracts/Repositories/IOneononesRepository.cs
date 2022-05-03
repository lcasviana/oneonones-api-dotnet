using Oneonones.Domain.Entities;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IOneononesRepository
    {
        Task<IList<OneononeEntity>> Obtain();
        Task<OneononeEntity> Obtain(string id);
        Task<IList<OneononeEntity>> ObtainByEmployee(string id);
        Task<OneononeEntity> ObtainByPair(string leaderId, string ledId);
        Task<bool> Insert(OneononeEntity oneononeEntity);
        Task<bool> Update(OneononeEntity oneononeEntity);
        Task<bool> Delete(string id);
    }
}