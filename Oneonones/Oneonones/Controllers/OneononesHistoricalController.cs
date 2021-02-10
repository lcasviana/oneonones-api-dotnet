using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;
using System;
using System.Linq;
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
        public async Task<IActionResult> ObtainAll([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            var oneononeHistoricalEntityList = await oneononesHistoricalService.ObtainAll(leaderEmail, ledEmail);
            var oneononeHistoricalViewModel = oneononeHistoricalEntityList.Select(h => h.ToViewModel());
            return Ok(oneononeHistoricalViewModel);
        }

        [HttpGet("{occurrence}")]
        public async Task<IActionResult> ObtainOccurrence([FromQuery] string leaderEmail, [FromQuery] string ledEmail, [FromRoute] DateTime occurrence)
        {
            var oneononeHistoricalEntity = await oneononesHistoricalService.ObtainOccurrence(leaderEmail, ledEmail, occurrence);
            var oneononeHistoricalViewModel = oneononeHistoricalEntity.ToViewModel();
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
        public async Task<IActionResult> Delete([FromQuery] string leaderEmail, [FromQuery] string ledEmail, [FromQuery] DateTime occurrence)
        {
            await oneononesHistoricalService.Delete(leaderEmail, ledEmail, occurrence);
            return NoContent();
        }
    }
}