using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesHistoricalDatabase
    {
        Task<IList<OneononeHistoricalModel>> ObtainAll(string leaderEmail, string ledEmail);
        Task<OneononeHistoricalModel> ObtainOccurrence(string leaderEmail, string ledEmail, DateTime occurrence);
        Task Insert(OneononeHistoricalModel oneonone);
        Task Update(OneononeHistoricalModel oneonone);
        Task Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}