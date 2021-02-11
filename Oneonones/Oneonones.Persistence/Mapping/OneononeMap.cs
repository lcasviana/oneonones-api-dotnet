using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Mapping
{
    public static class OneononeMap
    {
        public static OneononeModel ToModel(this OneononeEntity entity)
        {
            if (entity == null) return null;

            var model = new OneononeModel
            {
                LeaderEmail = entity.Leader.Email,
                LedEmail = entity.Led.Email,
                FrequencyType = (int)entity.Frequency,
            };

            return model;
        }

        public static OneononeEntity ToEntity(this OneononeModel model)
        {
            if (model == null) return null;

            var entity = new OneononeEntity
            {
                Leader = new EmployeeEntity { Email = model.LeaderEmail },
                Led = new EmployeeEntity { Email = model.LedEmail },
                Frequency = (OneononeFrequencyEnum)model.FrequencyType,
            };

            return entity;
        }
    }
}