using Oneonones.Persistence.Models;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesDatabase
    {
        Task<OneononeModel> Obtain(string leaderEmail, string ledEmail);
        Task Insert(OneononeModel oneonone);
        Task Update(OneononeModel oneonone);
        Task Delete(string leaderEmail, string ledEmail);
    }
}