using Meetings.Domain.Entities;
using Meetings.Service.Contract;
using System.Threading.Tasks;

namespace Meetings.Service.Implementation
{
    public class MeetingsService : IMeetingsService
    {
        public Task<MeetingEntity> Obtain(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(MeetingEntity meeting)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(MeetingEntity meeting)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(MeetingEntity meeting)
        {
            throw new System.NotImplementedException();
        }
    }
}