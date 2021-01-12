using Oneonones.Domain.Entities;
using Oneonones.Service.Contract;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementation
{
    public class OneononesService : IOneononesService
    {
        public Task<OneononeEntity> Obtain(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(OneononeEntity oneonone)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(OneononeEntity oneonone)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(OneononeEntity oneonone)
        {
            throw new System.NotImplementedException();
        }
    }
}