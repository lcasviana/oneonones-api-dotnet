using Meetings.Domain.Entities;
using System.Threading.Tasks;

namespace Meetings.Service.Contract
{
    public interface IMeetingsService
    {
        Task<MeetingEntity> Obtain(string email);
        Task Insert(MeetingEntity meeting);
        Task Update(MeetingEntity meeting);
        Task Delete(MeetingEntity meeting);
    }
}