using Oneonones.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Persistence.Contracts.Databases
{
    public interface IOneononesDatabase
    {
        Task<IList<OneononeModel>> Obtain();
        Task<OneononeModel> Obtain(string id);
        Task<IList<OneononeModel>> ObtainByEmployee(string id);
        Task<OneononeModel> ObtainByPair(string leaderId, string ledId);
        Task<int> Insert(OneononeModel oneonone);
        Task<int> Update(OneononeModel oneonone);
        Task<int> Delete(string id);
    }
}