using Oneonones.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Oneonones.Infrastructure.ViewModel;
using Oneonones.Infrastructure.Mapping;

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
        public async Task<IActionResult> ObtainOneonone([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            var oneononeEntity = await oneononesService.Obtain(leaderEmail, ledEmail);
            var oneononeModel = oneononeEntity.ToModel();
            return Ok(oneononeModel);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOneonone([FromBody] OneononeInputModel oneononeInputModel)
        {
            var oneononeInputEntity = oneononeInputModel.ToEntity();
            await oneononesService.Update(oneononeInputEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOneonone([FromBody] OneononeInputModel oneononeInputModel)
        {
            var oneononeInputEntity = oneononeInputModel.ToEntity();
            await oneononesService.Insert(oneononeInputEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOneonone([FromQuery] string leaderEmail, [FromQuery] string ledEmail)
        {
            await oneononesService.Delete(leaderEmail, ledEmail);
            return NoContent();
        }
    }
}