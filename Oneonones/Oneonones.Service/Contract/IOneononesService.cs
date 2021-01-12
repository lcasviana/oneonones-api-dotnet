using Oneonones.Domain.Entities;
using System.Threading.Tasks;

namespace Oneonones.Service.Contract
{
    public interface IOneononesService
    {
        Task<OneononeEntity> Obtain(string email);
        Task Insert(OneononeEntity oneonone);
        Task Update(OneononeEntity oneonone);
        Task Delete(OneononeEntity oneonone);
    }
}