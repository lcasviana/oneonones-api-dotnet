using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeHistoricalMap
    {
        public static OneononeHistoricalModel ToModel(this OneononeHistoricalEntity entity)
        {
            var model = new OneononeHistoricalModel
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