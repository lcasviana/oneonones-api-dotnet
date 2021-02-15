using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesHistoricalDatabase
    {
        Task<IList<OneononeHistoricalModel>> ObtainByPair(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalModel> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task Insert(OneononeHistoricalModel oneonone);
        Task Update(OneononeHistoricalModel oneonone);
        Task Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}