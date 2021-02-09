using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IOneononesHistoricalService
    {
        Task<OneononeHistoricalEntity> Obtain(string leaderEmail, string ledEmail);
        Task Insert(OneononeHistoricalInputEntity oneononeHistoricalInput);
        Task Update(OneononeHistoricalInputEntity oneononeHistoricalInput);
        Task Delete(string leaderEmail, string ledEmail);
    }
}