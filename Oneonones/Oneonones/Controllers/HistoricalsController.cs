using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/historicals")]
public class HistoricalsController : ControllerBase
{
    private readonly IHistoricalsService historicalsService;
    private readonly IEmployeesService employeesService;

    public HistoricalsController(
        IHistoricalsService historicalsService,
        IEmployeesService employeesService)
    {
        this.historicalsService = historicalsService;
        this.employeesService = employeesService;
    }

    [HttpGet]
    public async Task<IActionResult> Obtain(
        [FromQuery] string id, [FromQuery] string email,
        [FromQuery] string leaderId, [FromQuery] string ledId,
        [FromQuery] string leaderEmail, [FromQuery] string ledEmail,
        [FromQuery] DateTime? occurrence)
    {
        if (id != null)
            return await ObtainByEmployeeId(id);
        else if (email != null)
            return await ObtainByEmployeeEmail(email);
        else if (leaderId != null || ledId != null)
            return await ObtainByPairId(leaderId, ledId, occurrence);
        else if (leaderEmail != null || ledEmail != null)
            return await ObtainByPairEmail(leaderEmail, ledEmail, occurrence);
        else
            return await ObtainAll();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtain([FromRoute] string id)
    {
        var historicalEntity = await historicalsService.Obtain(id);
        var historicalViewModel = historicalEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, historicalViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] HistoricalInputViewModel historicalInputViewModel)
    {
        var historicalInputEntity = historicalInputViewModel.ToEntity();
        var historicalEntity = await historicalsService.Insert(historicalInputEntity);
        var historicalViewModel = historicalEntity.ToViewModel();
        return StatusCode(StatusCodes.Status201Created, historicalViewModel);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] HistoricalViewModel historicalViewModel)
    {
        var historicalEntity = historicalViewModel.ToEntity();
        var historicalEntityUpdated = await historicalsService.Update(historicalEntity);
        var historicalViewModelUpdated = historicalEntityUpdated.ToViewModel();
        return StatusCode(StatusCodes.Status202Accepted, historicalViewModelUpdated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await historicalsService.Delete(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    #region Obtain Filters

    private async Task<IActionResult> ObtainByEmployeeId(string id)
    {
        var historicalEntityList = await historicalsService.ObtainByEmployee(id);
        var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, historicalViewModelList);
    }

    private async Task<IActionResult> ObtainByEmployeeEmail(string email)
    {
        var employee = await employeesService.ObtainByEmail(email);
        var historicalEntityList = await historicalsService.ObtainByEmployee(employee.Id);
        var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, historicalViewModelList);
    }

    private async Task<IActionResult> ObtainByPairId(string leaderId, string ledId, DateTime? occurrence)
    {
        return occurrence != null
            ? await ObtainByPairIdOccurrence(leaderId, ledId, occurrence)
            : await ObtainByPairIdAll(leaderId, ledId);
    }

    private async Task<IActionResult> ObtainByPairIdOccurrence(string leaderId, string ledId, DateTime? occurrence)
    {
        var historicalEntity = await historicalsService.ObtainByOccurrence(leaderId, ledId, occurrence.Value);
        var historicalViewModel = historicalEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, historicalViewModel);
    }

    private async Task<IActionResult> ObtainByPairIdAll(string leaderId, string ledId)
    {
        var historicalEntityList = await historicalsService.ObtainByPair(leaderId, ledId);
        var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, historicalViewModelList);
    }

    private async Task<IActionResult> ObtainByPairEmail(string leaderEmail, string ledEmail, DateTime? occurrence)
    {
        return occurrence != null
            ? await ObtainByPairEmailOccurrence(leaderEmail, ledEmail, occurrence)
            : await ObtainByPairEmailAll(leaderEmail, ledEmail);
    }

    private async Task<IActionResult> ObtainByPairEmailOccurrence(string leaderEmail, string ledEmail, DateTime? occurrence)
    {
        var leader = await employeesService.ObtainByEmail(leaderEmail);
        var led = await employeesService.ObtainByEmail(ledEmail);
        var historicalEntity = await historicalsService.ObtainByOccurrence(leader.Id, led.Id, occurrence.Value);
        var historicalViewModel = historicalEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, historicalViewModel);
    }

    private async Task<IActionResult> ObtainByPairEmailAll(string leaderEmail, string ledEmail)
    {
        var leader = await employeesService.ObtainByEmail(leaderEmail);
        var led = await employeesService.ObtainByEmail(ledEmail);
        var historicalEntityList = await historicalsService.ObtainByPair(leader.Id, led.Id);
        var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, historicalViewModelList);
    }

    private async Task<IActionResult> ObtainAll()
    {
        var historicalEntityList = await historicalsService.Obtain();
        var historicalViewModelList = historicalEntityList.Select(HistoricalMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, historicalViewModelList);
    }

    #endregion
}
