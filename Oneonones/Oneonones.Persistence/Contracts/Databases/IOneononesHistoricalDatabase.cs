using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesHistoricalDatabase
    {
        Task<IList<OneononeHistoricalModel>> ObtainAll();
        Task<IList<OneononeHistoricalModel>> ObtainByEmployee(string email);
        Task<IList<OneononeHistoricalModel>> ObtainByPair(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalModel> ObtainByPairLast(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalModel> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task<int> Insert(OneononeHistoricalModel oneonone);
        Task<int> Update(OneononeHistoricalModel oneonone);
        Task<int> Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}