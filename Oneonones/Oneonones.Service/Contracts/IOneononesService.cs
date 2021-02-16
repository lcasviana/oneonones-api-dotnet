using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IOneononesService
    {
        Task<IList<OneononeEntity>> ObtainAll();
        Task<IList<OneononeEntity>> ObtainByEmployee(string email);
        Task<OneononeEntity> ObtainByPair(string leaderEmail, string ledEmail);
        Task Insert(OneononeInputEntity oneononeInput);
        Task Update(OneononeInputEntity oneononeInput);
        Task Delete(string leaderEmail, string ledEmail);
    }
}