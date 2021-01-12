﻿using Meetings.Domain.Entities;
using Meetings.Infrastructure.ViewModel;

namespace Meetings.Infrastructure.Mapping
{
    public static class MeetingMap
    {
        public static MeetingModel ToModel(this MeetingEntity entity)
        {
            var model = new MeetingModel
            {
                Leader = entity.Leader.ToModel(),
                Led = entity.Led.ToModel(),
                Frequency = entity.Frequency,
                LastMeeting = entity.LastMeeting,
            };
            return model;
        }
    }
}