using Meetings.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Meetings.Controllers
{
    [ApiController]
    [Route("api/v1/meetings")]
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
            return Ok(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        [HttpPost]
        public async Task<IActionResult> InsertMeeting(string email)
        {
            return Ok(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMeeting(string email)
        {
            return Ok(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMeeting(string email)
        {
            return Ok(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }
    }
}