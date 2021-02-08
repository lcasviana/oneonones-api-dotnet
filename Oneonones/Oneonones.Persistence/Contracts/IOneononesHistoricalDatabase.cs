using Oneonones.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts
{
    public interface IOneononesHistoricalDatabase
    {
        Task<IList<OneononeHistoricalModel>> Obtain(string leaderEmail, string ledEmail);
        Task Insert(OneononeHistoricalModel oneonone);
        Task Update(OneononeHistoricalModel oneonone);
        Task Delete(string leaderEmail, string ledEmail, DateTime occurrence);
    }
}