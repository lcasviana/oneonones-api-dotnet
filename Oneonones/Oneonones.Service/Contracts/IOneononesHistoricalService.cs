using Oneonones.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IOneononesHistoricalService
    {
        Task<IList<OneononeHistoricalEntity>> ObtainByPair(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalEntity> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task Insert(OneononeHistoricalInputEntity oneononeHistoricalInput);
        Task Update(OneononeHistoricalInputEntity oneononeHistoricalInput);
        Task Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}