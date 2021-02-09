using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Oneonones.Infrastructure.ViewModel;
using Oneonones.Infrastructure.Mapping;
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
        public async Task<IActionResult> Obtain([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            var oneononeEntity = await oneononesService.Obtain(leaderEmail, ledEmail);
            var oneononeModel = oneononeEntity.ToViewModel();
            return Ok(oneononeModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] OneononeInputViewModel oneononeInputViewModel)
        {
            var oneononeInputEntity = oneononeInputViewModel.ToEntity();
            await oneononesService.Update(oneononeInputEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OneononeInputViewModel oneononeInputViewModel)
        {
            var oneononeInputEntity = oneononeInputViewModel.ToEntity();
            await oneononesService.Insert(oneononeInputEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            await oneononesService.Delete(leaderEmail, ledEmail);
            return NoContent();
        }
    }
}