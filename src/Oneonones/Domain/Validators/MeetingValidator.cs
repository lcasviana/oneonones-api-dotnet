using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class MeetingInsertValidator : AbstractValidator<MeetingInsert>
{
    public MeetingInsertValidator()
    {
        RuleFor(meeting => meeting.LeaderId).NotEmpty();
        RuleFor(meeting => meeting.LedId).NotEmpty();
        RuleFor(meeting => meeting.MeetingDate).NotEmpty();
        RuleFor(meeting => meeting.Annotation).MaximumLength(2047);
    }
}

public class MeetingUpdateValidator : AbstractValidator<MeetingUpdate>
{
    public MeetingUpdateValidator()
    {
        RuleFor(meeting => meeting.MeetingDate).NotEmpty();
        RuleFor(meeting => meeting.Annotation).MaximumLength(2047);
    }
}
