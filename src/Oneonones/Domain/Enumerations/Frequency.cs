namespace Oneonones.Domain.Enumerations;

public enum Frequency
{
    Weekly = 7,
    Semimonthly = 15,
    Monthly = 30,
    Bimonthly = 60,
    Trimonthly = 90,
    Semiyearly = 180,
    Yearly = 365,
    Occasionally = 999,
}

public static class FrequencyExtension
{
    public static DateTime NextMeeting(this Frequency frequency, DateTime lastMeeting) => frequency switch
    {
        Frequency.Weekly => lastMeeting.AddDays(7),
        Frequency.Semimonthly => lastMeeting.AddDays(14),
        Frequency.Monthly => lastMeeting.AddMonths(1),
        Frequency.Bimonthly => lastMeeting.AddMonths(2),
        Frequency.Trimonthly => lastMeeting.AddMonths(3),
        Frequency.Semiyearly => lastMeeting.AddMonths(6),
        Frequency.Yearly => lastMeeting.AddYears(1),
        Frequency.Occasionally => DateTime.MaxValue,
        _ => DateTime.MinValue,
    };
}
