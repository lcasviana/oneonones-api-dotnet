using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class OneononesController : ControllerBase
{
    private readonly IOneononeService oneononeService;
    private readonly IValidator<OneononeInput> oneononeValidator;

    public OneononesController(
        IOneononeService oneononeService,
        IValidator<OneononeInput> oneononeValidator)
    {
        this.oneononeService = oneononeService;
        this.oneononeValidator = oneononeValidator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtainAllAsync()
    {
        var oneonones = await oneononeService.ObtainAllAsync();
        return Ok(oneonones);
    }

    [HttpGet("{oneononeId}")]
    public async Task<IActionResult> ObtainByIdAsync(Guid oneononeId)
    {
        var oneonone = await oneononeService.ObtainByIdAsync(oneononeId);
        return Accepted(oneonone);
    }

    public async Task<IActionResult> InsertAsync(OneononeInput oneononeInput)
    {
        var validation = oneononeValidator.Validate(oneononeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var guid = await oneononeService.InsertAsync(oneononeInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{oneononeId}")]
    public async Task<IActionResult> UpdateAsync(Guid oneononeId, OneononeInput oneononeInput)
    {
        var validation = oneononeValidator.Validate(oneononeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var oneonone = await oneononeService.UpdateAsync(oneononeId, oneononeInput);
        return Accepted(oneonone);
    }

    [HttpDelete("{oneononeId}")]
    public async Task<IActionResult> DeleteAsync(Guid oneononeId)
    {
        await oneononeService.DeleteAsync(oneononeId);
        return NoContent();
    }
}
