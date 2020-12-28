using Meetings.Domain.Settings;
using System.Threading.Tasks;

namespace Meetings.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
