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
        Task Insert(OneononeEntity oneonone);
        Task Update(OneononeEntity oneonone);
        Task Delete(string leaderEmail, string ledEmail);
    }
}