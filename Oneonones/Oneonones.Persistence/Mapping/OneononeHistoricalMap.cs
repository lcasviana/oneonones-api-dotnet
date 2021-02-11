using Oneonones.Domain.Entities;
using Oneonones.Persistence.Models;

namespace OneononeHistoricals.Persistence.Mapping
{
    public static class OneononeHistoricalHistoricalMap
    {
        public static OneononeHistoricalModel ToModel(this OneononeHistoricalEntity entity)
        {
            if (entity == null) return null;

            var model = new OneononeHistoricalModel
            {
                LeaderEmail = entity.Leader.Email,
                LedEmail = entity.Led.Email,
                Occurrence = entity.Occurrence,
                Commentary = entity.Commentary,
            };

            return model;
        }

        public static OneononeHistoricalEntity ToEntity(this OneononeHistoricalModel model)
        {
            if (model == null) return null;

            var entity = new OneononeHistoricalEntity
            {
                Leader = new EmployeeEntity { Email = model.LeaderEmail },
                Led = new EmployeeEntity { Email = model.LedEmail },
                Occurrence = model.Occurrence,
                Commentary = model.Commentary,
            };

            return entity;
        }
    }
}