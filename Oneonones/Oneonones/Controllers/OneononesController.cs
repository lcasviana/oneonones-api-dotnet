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
    [Route("api/v1/oneonones")]
    public class OneononesController : ControllerBase
    {
        private readonly IOneononesService oneononesService;

        public OneononesController(IOneononesService oneononesService)
        {
            this.oneononesService = oneononesService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtain(
            [FromQuery] string id, [FromQuery] string email,
            [FromQuery] string leaderId, [FromQuery] string ledId,
            [FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            if (id != null)
                return await ObtainByEmployeeId(id);
            else if (email != null)
                return await ObtainByEmployeeEmail(email);
            else if (leaderId != null || ledId != null)
                return await ObtainByPairId(leaderId, ledId);
            else if (leaderEmail != null || ledEmail != null)
                return await ObtainByPairEmail(leaderEmail, ledEmail);
            else
                return await ObtainAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtain([FromRoute] string id)
        {
            var oneononeEntity = await oneononesService.Obtain(id);
            var oneononeViewModel = oneononeEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, oneononeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OneononeInputViewModel oneononeInputViewModel)
        {
            var oneononeInputEntity = oneononeInputViewModel.ToEntity();
            var oneononeEntity = await oneononesService.Insert(oneononeInputEntity);
            var oneononeViewModel = oneononeEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status201Created, oneononeViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OneononeViewModel oneononeViewModel)
        {
            var oneononeEntity = oneononeViewModel.ToEntity();
            var oneononeEntityUpdated = await oneononesService.Update(oneononeEntity);
            var oneononeViewModelUpdated = oneononeEntityUpdated.ToViewModel();
            return StatusCode((int)StatusCodes.Status202Accepted, oneononeViewModelUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await oneononesService.Delete(id);
            return StatusCode((int)StatusCodes.Status204NoContent);
        }

        #region Obtain Filters

        private async Task<IActionResult> ObtainByEmployeeId(string id)
        {
            var oneononeEntityList = await oneononesService.ObtainByEmployee(id);
            var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
            return StatusCode((int)StatusCodes.Status200OK, oneononeViewModelList);
        }

        private async Task<IActionResult> ObtainByEmployeeEmail(string email)
        {
            throw new System.NotImplementedException("Query by id.");
        }

        private async Task<IActionResult> ObtainByPairId(string leaderId, string ledId)
        {
            var oneononeEntity = await oneononesService.ObtainByPair(leaderId, ledId);
            var oneononeViewModel = oneononeEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, oneononeViewModel);
        }

        private async Task<IActionResult> ObtainByPairEmail(string leaderEmail, string ledEmail)
        {
            throw new System.NotImplementedException("Query by id.");
        }

        private async Task<IActionResult> ObtainAll()
        {
            var oneononeEntityList = await oneononesService.Obtain();
            var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
            return StatusCode((int)StatusCodes.Status200OK, oneononeViewModelList);
        }

        #endregion
    }
}