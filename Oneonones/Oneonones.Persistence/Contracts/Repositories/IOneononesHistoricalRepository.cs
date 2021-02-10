using Oneonones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Repositories
{
    public interface IOneononesHistoricalRepository
    {
        Task<IList<OneononeHistoricalEntity>> ObtainAll(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalEntity> ObtainOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task Insert(OneononeHistoricalEntity oneonone);
        Task Update(OneononeHistoricalEntity oneonone);
        Task Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}