using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers
{
    [ApiController]
    [Route("api/v1/dashboards")]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardsService dashboardsService;

        public DashboardsController(IDashboardsService dashboardsService)
        {
            this.dashboardsService = dashboardsService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtain([FromQuery] string email)
        {
            return email != null
                ? await ObtainByEmail(email)
                : await ObtainAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtainById([FromRoute] string id)
        {
            var dashboardEntity = await dashboardsService.Obtain(id);
            var dashboardViewModel = dashboardEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, dashboardViewModel);
        }

        #region Obtain Filters

        private async Task<IActionResult> ObtainByEmail(string email)
        {
            throw new System.NotImplementedException("Query by id.");
        }

        private async Task<IActionResult> ObtainAll()
        {
            var dashboardEntityList = await dashboardsService.Obtain();
            var dashboardViewModelList = dashboardEntityList.Select(DashboardMap.ToViewModel).ToList();
            return StatusCode((int)StatusCodes.Status200OK, dashboardViewModelList);
        }

        #endregion
    }
}