using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IOneononesRepository
    {
        Task<OneononeEntity> Obtain(string leaderEmail, string ledEmail);
        Task Insert(OneononeEntity oneonone);
        Task Update(OneononeEntity oneonone);
        Task Delete(string leaderEmail, string ledEmail);
    }
}