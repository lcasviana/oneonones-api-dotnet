namespace Oneonones.Domain.Outputs;

public class StatusOutput
{
    /// <summary>
    /// One-on-one's last meeting
    /// </summary>
    /// <example>
    /// 2022-09-10
    /// </example>
    public DateTime? LastMeeting { get; set; }
    /// <summary>
    /// One-on-one's next meeting
    /// </summary>
    /// <example>
    /// 2022-10-10
    /// </example>
    public DateTime? NextMeeting { get; set; }
    /// <summary>
    /// One-on-one's status
    /// </summary>
    /// <example>
    /// true
    /// </example>
    public bool? IsLate { get; set; }


    public StatusOutput(OneononeOutput oneonone)
    {
        if (oneonone.Meetings?.Any() == true)
        {
            LastMeeting = oneonone.Meetings.Max(meeting => meeting.MeetingDate);

        }
    }
}
