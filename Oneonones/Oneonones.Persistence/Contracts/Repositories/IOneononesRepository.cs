using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IOneononesRepository
    {
        Task<IList<OneononeEntity>> ObtainAll();
        Task<IList<OneononeEntity>> ObtainByEmployee(string email);
        Task<OneononeEntity> ObtainByPair(string leaderEmail, string ledEmail);
        Task<bool> Insert(OneononeEntity oneonone);
        Task<bool> Update(OneononeEntity oneonone);
        Task<bool> Delete(string leaderEmail, string ledEmail);
    }
}