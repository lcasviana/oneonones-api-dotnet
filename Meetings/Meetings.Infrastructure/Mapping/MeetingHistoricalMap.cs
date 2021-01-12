using Meetings.Domain.Entities;
using Meetings.Infrastructure.ViewModel;

namespace Meetings.Infrastructure.Mapping
{
    public static class MeetingHistoricalMap
    {
        public static MeetingHistoricalModel ToModel(this MeetingHistoricalEntity entity)
        {
            var model = new MeetingHistoricalModel
            {
                Leader = entity.Leader.ToModel(),
                Led = entity.Led.ToModel(),
                Occurrence = entity.Occurrence,
                Commentary = entity.Commentary,
            };
            return model;
        }
    }
}