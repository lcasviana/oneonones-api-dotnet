using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class OneononesController : ControllerBase
{
    private readonly IOneononeService oneononeService;
    private readonly IValidator<OneononeInsert> oneononeInsertValidator;
    private readonly IValidator<OneononeUpdate> oneononeUpdateValidator;

    public OneononesController(
        IOneononeService oneononeService,
        IValidator<OneononeInsert> oneononeInsertValidator,
        IValidator<OneononeUpdate> oneononeUpdateValidator)
    {
        this.oneononeService = oneononeService;
        this.oneononeInsertValidator = oneononeInsertValidator;
        this.oneononeUpdateValidator = oneononeUpdateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OneononeOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainAllAsync()
    {
        var oneonones = await oneononeService.ObtainAllAsync();
        return Ok(oneonones.Select(oneonone => (OneononeOutput)oneonone));
    }

    [HttpGet("{oneononeId}")]
    [ProducesResponseType(typeof(OneononeOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainByIdAsync([FromRoute] Guid oneononeId)
    {
        var oneonone = await oneononeService.ObtainByIdAsync(oneononeId);
        return Ok((OneononeOutput)oneonone);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertAsync([FromBody] OneononeInsert oneononeInput)
    {
        var validation = oneononeInsertValidator.Validate(oneononeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var guid = await oneononeService.InsertAsync(oneononeInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{oneononeId}")]
    [ProducesResponseType(typeof(OneononeOutput), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid oneononeId, [FromBody] OneononeUpdate oneononeInput)
    {
        var validation = oneononeUpdateValidator.Validate(oneononeInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var oneonone = await oneononeService.UpdateAsync(oneononeId, oneononeInput);
        return Accepted(oneonone);
    }

    [HttpDelete("{oneononeId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid oneononeId)
    {
        await oneononeService.DeleteAsync(oneononeId);
        return NoContent();
    }
}
