using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers
{
    [ApiController]
    [Route("api/v1/historicals")]
    public class HistoricalsController : ControllerBase
    {
        private readonly IHistoricalsService historicalsService;

        public HistoricalsController(IHistoricalsService historicalsService)
        {
            this.historicalsService = historicalsService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtainAll(
            [FromQuery] string email,
            [FromQuery] string leaderEmail, [FromQuery] string ledEmail,
            [FromQuery] string leaderId, [FromQuery] string ledId,
            [FromQuery] DateTime? occurrence)
        {
            if (email != null)
            {
                var historicalEntityList = await historicalsService.ObtainByEmployee(email);
                var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
                return Ok(historicalViewModelList);
            }
            else if (leaderEmail != null || ledEmail != null)
            {
                throw new System.NotImplementedException("Query by id.");
            }
            else if (leaderId != null || ledId != null)
            {
                if (occurrence != null)
                {
                    var historicalEntity = await historicalsService.ObtainByOccurrence(leaderId, ledId, occurrence.Value);
                    var historicalViewModel = historicalEntity.ToViewModel();
                    return Ok(historicalViewModel);
                }
                else
                {
                    var historicalEntityList = await historicalsService.ObtainByPair(leaderId, ledId);
                    var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
                    return Ok(historicalViewModelList);
                }
            }
            else
            {
                var historicalEntityList = await historicalsService.Obtain();
                var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
                return Ok(historicalViewModelList);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtainById([FromRoute] string id)
        {
            var historicalEntity = await historicalsService.Obtain(id);
            var historicalViewModel = historicalEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, historicalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] HistoricalInputViewModel historicalInputViewModel)
        {
            var historicalInputEntity = historicalInputViewModel.ToEntity();
            var historicalEntity = await historicalsService.Insert(historicalInputEntity);
            var historicalViewModel = historicalEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status201Created, historicalViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HistoricalViewModel historicalViewModel)
        {
            var historicalEntity = historicalViewModel.ToEntity();
            var historicalEntityUpdated = await historicalsService.Update(historicalEntity);
            var historicalViewModelUpdated = historicalEntityUpdated.ToViewModel();
            return StatusCode((int)StatusCodes.Status202Accepted, historicalViewModelUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await historicalsService.Delete(id);
            return StatusCode((int)StatusCodes.Status204NoContent);
        }
    }
}