using Meetings.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingsService meetingsService;

        public MeetingsController(IMeetingsService meetingsService)
        {
            this.meetingsService = meetingsService;
        }
    }
}