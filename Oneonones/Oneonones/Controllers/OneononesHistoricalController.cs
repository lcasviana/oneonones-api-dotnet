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
        public async Task<IActionResult> ObtainAll()
        {
            var oneononeHistoricalEntityList = await oneononesHistoricalService.ObtainAll();
            var oneononeHistoricalViewModelList = oneononeHistoricalEntityList.Select(OneononeHistoricalMap.ToViewModel).ToList();
            return Ok(oneononeHistoricalViewModelList);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ObtainByEmployee([FromRoute] string email)
        {
            var oneononeHistoricalEntityList = await oneononesHistoricalService.ObtainByEmployee(email);
            var oneononeHistoricalViewModelList = oneononeHistoricalEntityList.Select(OneononeHistoricalMap.ToViewModel).ToList();
            return Ok(oneononeHistoricalViewModelList);
        }

        [HttpGet("{leaderEmail}/{ledEmail}")]
        public async Task<IActionResult> ObtainByPair([FromRoute] string leaderEmail, [FromRoute] string ledEmail)
        {
            var oneononeHistoricalEntityList = await oneononesHistoricalService.ObtainByPair(leaderEmail, ledEmail);
            var oneononeHistoricalViewModelList = oneononeHistoricalEntityList.Select(OneononeHistoricalMap.ToViewModel).ToList();
            return Ok(oneononeHistoricalViewModelList);
        }

        [HttpGet("{leaderEmail}/{ledEmail}/{occurrence}")]
        public async Task<IActionResult> ObtainByPairOccurrence([FromRoute] string leaderEmail, [FromRoute] string ledEmail, [FromRoute] DateTime occurrence)
        {
            var oneononeHistoricalEntity = await oneononesHistoricalService.ObtainByPairOccurrence(leaderEmail, ledEmail, occurrence);
            var oneononeHistoricalViewModel = oneononeHistoricalEntity.ToViewModel();
            return Ok(oneononeHistoricalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OneononeHistoricalInputViewModel oneononeHistoricalInputModel)
        {
            var oneononeHistoricalInputEntity = oneononeHistoricalInputModel.ToEntity();
            await oneononesHistoricalService.Insert(oneononeHistoricalInputEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OneononeHistoricalInputViewModel oneononeHistoricalInputModel)
        {
            var oneononeHistoricalInputEntity = oneononeHistoricalInputModel.ToEntity();
            await oneononesHistoricalService.Update(oneononeHistoricalInputEntity);
            return NoContent();
        }

        [HttpDelete("{leaderEmail}/{ledEmail}/{occurrence}")]
        public async Task<IActionResult> Delete([FromRoute] string leaderEmail, [FromRoute] string ledEmail, [FromRoute] DateTime occurrence)
        {
            await oneononesHistoricalService.Delete(leaderEmail, ledEmail, occurrence);
            return NoContent();
        }
    }
}