using System.Collections.Generic;
using System.Threading.Tasks;
using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IOneononesService
    {
        Task<IList<OneononeEntity>> Obtain();
        Task<OneononeEntity> Obtain(string id);
        Task<IList<OneononeEntity>> ObtainByEmployee(string id);
        Task<OneononeEntity> ObtainByPair(string leaderId, string ledId);
        Task<OneononeEntity> Insert(OneononeInputEntity oneononeInput);
        Task<OneononeEntity> Update(OneononeEntity oneonone);
        Task Delete(string id);
    }
}