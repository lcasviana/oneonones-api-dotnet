using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingService meetingService;
    private readonly IValidator<MeetingInsert> meetingInsertValidator;
    private readonly IValidator<MeetingUpdate> meetingUpdateValidator;

    public MeetingsController(
        IMeetingService meetingService,
        IValidator<MeetingInsert> meetingInsertValidator,
        IValidator<MeetingUpdate> meetingUpdateValidator)
    {
        this.meetingService = meetingService;
        this.meetingInsertValidator = meetingInsertValidator;
        this.meetingUpdateValidator = meetingUpdateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MeetingOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainAllAsync()
    {
        var meetings = await meetingService.ObtainAllAsync();
        return Ok(meetings.Select(meeting => (MeetingOutput)meeting));
    }

    [HttpGet("{meetingId}")]
    [ProducesResponseType(typeof(MeetingOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtainByIdAsync([FromRoute] Guid meetingId)
    {
        var meeting = await meetingService.ObtainByIdAsync(meetingId);
        return Ok((MeetingOutput)meeting);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertAsync([FromBody] MeetingInsert meetingInput)
    {
        var validation = meetingInsertValidator.Validate(meetingInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var guid = await meetingService.InsertAsync(meetingInput);
        return CreatedAtAction(nameof(InsertAsync), guid);
    }

    [HttpPut("{meetingId}")]
    [ProducesResponseType(typeof(MeetingOutput), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid meetingId, [FromBody] MeetingUpdate meetingInput)
    {
        var validation = meetingUpdateValidator.Validate(meetingInput);
        if (!validation.IsValid) throw new InvalidException(validation.Errors);
        var meeting = await meetingService.UpdateAsync(meetingId, meetingInput);
        return Accepted((MeetingOutput)meeting);
    }

    [HttpDelete("{meetingId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid meetingId)
    {
        await meetingService.DeleteAsync(meetingId);
        return NoContent();
    }
}
