using Meetings.Service.Contract;
using System;

namespace Meetings.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}