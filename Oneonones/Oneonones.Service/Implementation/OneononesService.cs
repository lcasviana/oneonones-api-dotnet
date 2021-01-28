using Oneonones.Domain.Entities;
using Oneonones.Service.Contract;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementation
{
    public class OneononesService : IOneononesService
    {
        public Task<OneononeEntity> Obtain(string leaderEmail, string ledEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(OneononeInputEntity oneonone)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(OneononeInputEntity oneonone)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string leaderEmail, string ledEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}