using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class MeetingValidator : AbstractValidator<MeetingInput>
{
    public MeetingValidator()
    {
        RuleFor(meeting => meeting.LeaderId).NotEmpty();
        RuleFor(meeting => meeting.LedId).NotEmpty();
        RuleFor(meeting => meeting.MeetingDate).NotEmpty();
        RuleFor(meeting => meeting.Annotation).MaximumLength(2047);
    }
}
