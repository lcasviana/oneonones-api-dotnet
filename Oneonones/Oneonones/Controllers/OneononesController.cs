using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModel;
using Oneonones.Service.Contracts;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ObtainAll()
        {
            var oneononeEntityList = await oneononesService.ObtainAll();
            var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
            return Ok(oneononeViewModelList);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ObtainByEmployee([FromRoute] string email)
        {
            var oneononeEntityList = await oneononesService.ObtainByEmployee(email);
            var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
            return Ok(oneononeViewModelList);
        }

        [HttpGet("{leaderEmail}/{ledEmail}")]
        public async Task<IActionResult> ObtainByPair([FromRoute] string leaderEmail, [FromRoute] string ledEmail)
        {
            var oneononeEntity = await oneononesService.ObtainByPair(leaderEmail, ledEmail);
            var oneononeViewModel = oneononeEntity.ToViewModel();
            return Ok(oneononeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OneononeInputViewModel oneononeInputViewModel)
        {
            var oneononeInputEntity = oneononeInputViewModel.ToEntity();
            await oneononesService.Insert(oneononeInputEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OneononeInputViewModel oneononeInputViewModel)
        {
            var oneononeInputEntity = oneononeInputViewModel.ToEntity();
            await oneononesService.Update(oneononeInputEntity);
            return NoContent();
        }

        [HttpDelete("{leaderEmail}/{ledEmail}")]
        public async Task<IActionResult> Delete([FromRoute] string leaderEmail, [FromRoute] string ledEmail)
        {
            await oneononesService.Delete(leaderEmail, ledEmail);
            return NoContent();
        }
    }
}