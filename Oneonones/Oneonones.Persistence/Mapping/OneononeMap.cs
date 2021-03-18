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
                Id = entity.Id,
                LeaderId = entity.Leader.Email,
                LedId = entity.Led.Email,
                Frequency = (int)entity.Frequency,
            };

            return model;
        }

        public static OneononeEntity ToEntity(this OneononeModel model)
        {
            if (model == null) return null;

            var entity = new OneononeEntity
            {
                Id = model.Id,
                Leader = new EmployeeEntity { Id = model.LeaderId },
                Led = new EmployeeEntity { Id = model.LedId },
                Frequency = (FrequencyEnum)model.Frequency,
            };

            return entity;
        }
    }
}