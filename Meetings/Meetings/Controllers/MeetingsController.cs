using Meetings.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> ObtainMeeting(string email)
        {
            var meetingEntity = await meetingsService.Obtain(email);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> InsertMeeting(string email)
        {
            return Ok(await meetingsService.Obtain(email));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMeeting(string email)
        {
            return Ok(await meetingsService.Obtain(email));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMeeting(string email)
        {
            return Ok(await meetingsService.Obtain(email));
        }
    }
}