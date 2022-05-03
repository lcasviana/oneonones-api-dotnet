using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/dashboards")]
public class DashboardsController : ControllerBase
{
    private readonly IDashboardsService dashboardsService;

    public DashboardsController(IDashboardsService dashboardsService)
    {
        this.dashboardsService = dashboardsService;
    }

    [HttpGet]
    public async Task<IActionResult> Obtain([FromQuery] string id, [FromQuery] string email)
    {
        if (id != null)
            return await ObtainById(id);
        else if (email != null)
            return await ObtainByEmail(email);
        else
            return await ObtainAll();
    }

    #region Obtain Filters

    private async Task<IActionResult> ObtainById(string id)
    {
        var dashboardEntity = await dashboardsService.Obtain(id);
        var dashboardViewModel = dashboardEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, dashboardViewModel);
    }

    private async Task<IActionResult> ObtainByEmail(string email)
    {
        var dashboardEntity = await dashboardsService.ObtainByEmail(email);
        var dashboardViewModel = dashboardEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, dashboardViewModel);
    }

    private async Task<IActionResult> ObtainAll()
    {
        var dashboardEntityList = await dashboardsService.Obtain();
        var dashboardViewModelList = dashboardEntityList.Select(DashboardMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, dashboardViewModelList);
    }

    #endregion
}
