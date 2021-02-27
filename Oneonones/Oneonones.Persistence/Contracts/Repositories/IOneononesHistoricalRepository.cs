using Oneonones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IOneononesHistoricalRepository
    {
        Task<IList<OneononeHistoricalEntity>> ObtainAll();
        Task<IList<OneononeHistoricalEntity>> ObtainByEmployee(string email);
        Task<IList<OneononeHistoricalEntity>> ObtainByPair(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalEntity> ObtainByPairLast(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalEntity> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task<bool> Insert(OneononeHistoricalEntity oneonone);
        Task<bool> Update(OneononeHistoricalEntity oneonone);
        Task<bool> Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}