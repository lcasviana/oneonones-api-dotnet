using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;
using System.Threading.Tasks;

namespace Oneonones.Controllers
{
    [ApiController]
    [Route("api/v1/historicals")]
    public class OneononesHistoricalController : ControllerBase
    {
        private readonly IOneononesHistoricalService oneononesHistoricalService;

        public OneononesHistoricalController(IOneononesHistoricalService oneononesHistoricalService)
        {
            this.oneononesHistoricalService = oneononesHistoricalService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtain([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            var oneononeEntity = await oneononesHistoricalService.Obtain(leaderEmail, ledEmail);
            var oneononeHistoricalViewModel = oneononeEntity.ToViewModel();
            return Ok(oneononeHistoricalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OneononeHistoricalInputViewModel oneononeHistoricalInputModel)
        {
            var oneononeHistoricalInputEntity = oneononeHistoricalInputModel.ToEntity();
            await oneononesHistoricalService.Update(oneononeHistoricalInputEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OneononeHistoricalInputViewModel oneononeHistoricalInputModel)
        {
            var oneononeHistoricalInputEntity = oneononeHistoricalInputModel.ToEntity();
            await oneononesHistoricalService.Insert(oneononeHistoricalInputEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            await oneononesHistoricalService.Delete(leaderEmail, ledEmail);
            return NoContent();
        }
    }
}