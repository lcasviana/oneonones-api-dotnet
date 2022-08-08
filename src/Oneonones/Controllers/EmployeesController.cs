using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
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
    [ProducesResponseType(typeof(IEnumerable<EmployeeOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainAsync()
    {
        var employees = await employeesService.ObtainAllAsync();
        return Ok(employees.Select(employee => (EmployeeOutput) employee));
    }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(typeof(EmployeeOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainByIdAsync([FromRoute] Guid employeeId)
    {
        var employee = await employeesService.ObtainByIdAsync(employeeId);
        return Ok((EmployeeOutput) employee);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertAsync([FromBody] EmployeeInput employeeInput)
    {
        var validation = employeeValidator.Validate(employeeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var guid = await employeesService.InsertAsync(employeeInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{employeeId}")]
    [ProducesResponseType(typeof(EmployeeOutput), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid employeeId, [FromBody] EmployeeInput employeeInput)
    {
        var validation = employeeValidator.Validate(employeeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var employee = await employeesService.UpdateAsync(employeeId, employeeInput);
        return Accepted((EmployeeOutput) employee);
    }

    [HttpDelete("{employeeId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid employeeId)
    {
        await employeesService.DeleteAsync(employeeId);
        return NoContent();
    }
}
