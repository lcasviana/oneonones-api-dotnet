using Oneonones.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesDatabase
    {
        Task<IList<OneononeModel>> ObtainAll();
        Task<IList<OneononeModel>> ObtainByEmployee(string email);
        Task<OneononeModel> ObtainByPair(string leaderEmail, string ledEmail);
        Task Insert(OneononeModel oneonone);
        Task Update(OneononeModel oneonone);
        Task Delete(string leaderEmail, string ledEmail);
    }
}