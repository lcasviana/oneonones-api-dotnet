using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/oneonones")]
public class OneononesController : ControllerBase
{
    private readonly IOneononesService oneononesService;
    private readonly IEmployeesService employeesService;

    public OneononesController(
        IOneononesService oneononesService,
        IEmployeesService employeesService)
    {
        this.oneononesService = oneononesService;
        this.employeesService = employeesService;
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
        return StatusCode(StatusCodes.Status200OK, oneononeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] OneononeInputViewModel oneononeInputViewModel)
    {
        var oneononeInputEntity = oneononeInputViewModel.ToEntity();
        var oneononeEntity = await oneononesService.Insert(oneononeInputEntity);
        var oneononeViewModel = oneononeEntity.ToViewModel();
        return StatusCode(StatusCodes.Status201Created, oneononeViewModel);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] OneononeViewModel oneononeViewModel)
    {
        var oneononeEntity = oneononeViewModel.ToEntity();
        var oneononeEntityUpdated = await oneononesService.Update(oneononeEntity);
        var oneononeViewModelUpdated = oneononeEntityUpdated.ToViewModel();
        return StatusCode(StatusCodes.Status202Accepted, oneononeViewModelUpdated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await oneononesService.Delete(id);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    #region Obtain Filters

    private async Task<IActionResult> ObtainByEmployeeId(string id)
    {
        var oneononeEntityList = await oneononesService.ObtainByEmployee(id);
        var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, oneononeViewModelList);
    }

    private async Task<IActionResult> ObtainByEmployeeEmail(string email)
    {
        var employee = await employeesService.ObtainByEmail(email);
        var oneononeEntityList = await oneononesService.ObtainByEmployee(employee.Id);
        var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, oneononeViewModelList);
    }

    private async Task<IActionResult> ObtainByPairId(string leaderId, string ledId)
    {
        var oneononeEntity = await oneononesService.ObtainByPair(leaderId, ledId);
        var oneononeViewModel = oneononeEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, oneononeViewModel);
    }

    private async Task<IActionResult> ObtainByPairEmail(string leaderEmail, string ledEmail)
    {
        var leader = await employeesService.ObtainByEmail(leaderEmail);
        var led = await employeesService.ObtainByEmail(ledEmail);
        var oneononeEntity = await oneononesService.ObtainByPair(leader.Id, led.Id);
        var oneononeViewModel = oneononeEntity.ToViewModel();
        return StatusCode(StatusCodes.Status200OK, oneononeViewModel);
    }

    private async Task<IActionResult> ObtainAll()
    {
        var oneononeEntityList = await oneononesService.Obtain();
        var oneononeViewModelList = oneononeEntityList.Select(OneononeMap.ToViewModel).ToList();
        return StatusCode(StatusCodes.Status200OK, oneononeViewModelList);
    }

    #endregion
}
