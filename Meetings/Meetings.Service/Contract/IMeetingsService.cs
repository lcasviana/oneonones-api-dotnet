using System.Threading.Tasks;

namespace Meetings.Service.Contract
{
    public interface IMeetingsService
    {
        Task Obtain();
        Task Insert();
        Task Update();
        Task Delete();
    }
}