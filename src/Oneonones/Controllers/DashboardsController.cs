using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Outputs;
using Oneonones.Services.Contracts;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    private readonly IMeetingService meetingService;
    private readonly IOneononeService oneononeService;

    public DashboardsController(
        IEmployeeService employeeService,
        IMeetingService meetingService,
        IOneononeService oneononeService)
    {
        this.employeeService = employeeService;
        this.meetingService = meetingService;
        this.oneononeService = oneononeService;
    }

    [HttpGet("{employeeEmail}")]
    public async Task<IActionResult> ObtainByEmailAsync([FromRoute] string employeeEmail)
    {
        var employee = (EmployeeOutput) await employeeService.ObtainByEmailAsync(employeeEmail);
        var oneonones = (await oneononeService.ObtainByEmployeeAsync(employee.Id)).Select(oneonone => (OneononeOutput) oneonone);
        var tasks = oneonones.Select(async oneonone =>
        {
            oneonone.Meetings = (await meetingService.ObtainByOneononeAsync(oneonone.Leader.Id, oneonone.Led.Id)).Select(meeting => (MeetingOutput) meeting);
            var status = new StatusOutput(oneonone);
            return oneonone;
        });
        oneonones = await Task.WhenAll(tasks);
        var dashboard = new DashboardOutput(employee, oneonones);
        return Ok(dashboard);
    }
}
