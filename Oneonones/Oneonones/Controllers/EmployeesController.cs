using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService employeesService;
    private readonly IValidator<EmployeeInput> employeeValidator;

    public EmployeesController(
        IEmployeeService employeesService,
        IValidator<EmployeeInput> employeeValidator)
    {
        this.employeesService = employeesService;
        this.employeeValidator = employeeValidator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtainAsync([FromQuery] string? employeeEmail)
    {
        return employeeEmail is null
            ? await ObtainAllAsync()
            : await ObtainByEmailAsync(employeeEmail);
    }

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> ObtainByIdAsync([FromRoute] Guid employeeId)
    {
        var employee = await employeesService.ObtainByIdAsync(employeeId);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] EmployeeInput employeeInput)
    {
        var validation = employeeValidator.Validate(employeeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var guid = await employeesService.InsertAsync(employeeInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{employeeId}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid employeeId, [FromBody] EmployeeInput employeeInput)
    {
        var validation = employeeValidator.Validate(employeeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var employee = await employeesService.UpdateAsync(employeeId, employeeInput);
        return Accepted(employee);
    }

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid employeeId)
    {
        await employeesService.DeleteAsync(employeeId);
        return NoContent();
    }

    private async Task<IActionResult> ObtainAllAsync()
    {
        var employees = await employeesService.ObtainAllAsync();
        return Ok(employees);
    }

    private async Task<IActionResult> ObtainByEmailAsync(string employeeEmail)
    {
        var employee = await employeesService.ObtainByEmailAsync(employeeEmail);
        return Ok(employee);
    }
}
