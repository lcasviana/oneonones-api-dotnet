using Oneonones.Domain.Entities;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Mapping
{
    public static class HistoricalMap
    {
        public static HistoricalModel ToModel(this HistoricalEntity entity)
        {
            if (entity == null) return null;

            var model = new HistoricalModel
            {
                Id = entity.Id,
                LeaderId = entity.Leader.Id,
                LedId = entity.Led.Id,
                Occurrence = entity.Occurrence,
                Commentary = entity.Commentary,
            };

            return model;
        }

        public static HistoricalEntity ToEntity(this HistoricalModel model)
        {
            if (model == null) return null;

            var entity = new HistoricalEntity
            {
                Id = model.Id,
                Leader = new EmployeeEntity { Id = model.LeaderId },
                Led = new EmployeeEntity { Id = model.LedId },
                Occurrence = model.Occurrence,
                Commentary = model.Commentary,
            };

            return entity;
        }
    }
}