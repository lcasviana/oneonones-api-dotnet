using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Service.Contract
{
    public interface IOneononesService
    {
        Task<OneononeEntity> Obtain(string leaderEmail, string ledEmail);
        Task Insert(OneononeInputEntity oneononeInput);
        Task Update(OneononeInputEntity oneononeInput);
        Task Delete(string leaderEmail, string ledEmail);
    }
}