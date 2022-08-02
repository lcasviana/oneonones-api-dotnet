using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingService meetingService;
    private readonly IValidator<MeetingInput> meetingValidator;

    public MeetingsController(
        IMeetingService meetingService,
        IValidator<MeetingInput> meetingValidator)
    {
        this.meetingService = meetingService;
        this.meetingValidator = meetingValidator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtainAllAsync()
    {
        var meetings = await meetingService.ObtainAllAsync();
        return Ok(meetings);
    }

    [HttpGet("{meetingId}")]
    public async Task<IActionResult> ObtainByIdAsync([FromRoute] Guid meetingId)
    {
        var meeting = await meetingService.ObtainByIdAsync(meetingId);
        return Ok(meeting);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] MeetingInput meetingInput)
    {
        var validation = meetingValidator.Validate(meetingInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var guid = await meetingService.InsertAsync(meetingInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{meetingId}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid meetingId, [FromBody] MeetingInput meetingInput)
    {
        var validation = meetingValidator.Validate(meetingInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);

        var meeting = await meetingService.UpdateAsync(meetingId, meetingInput);
        return Accepted(meeting);
    }

    [HttpDelete("{meetingId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid meetingId)
    {
        await meetingService.DeleteAsync(meetingId);
        return NoContent();
    }
}
