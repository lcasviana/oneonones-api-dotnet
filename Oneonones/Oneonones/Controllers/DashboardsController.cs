using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Service.Contracts;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ObtainAll()
        {
            var dashboardEntityList = await dashboardsService.ObtainAll();
            var dashboardViewModelList = dashboardEntityList.Select(DashboardMap.ToViewModel).ToList();
            return Ok(dashboardViewModelList);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ObtainByEmployee([FromRoute] string email)
        {
            var dashboardEntity = await dashboardsService.ObtainByEmployee(email);
            var dashboardViewModel = dashboardEntity.ToViewModel();
            return Ok(dashboardViewModel);
        }
    }
}