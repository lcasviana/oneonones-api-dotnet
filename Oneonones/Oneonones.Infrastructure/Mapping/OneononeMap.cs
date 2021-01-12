using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeMap
    {
        public static OneononeModel ToModel(this OneononeEntity entity)
        {
            var model = new OneononeModel
            {
                Leader = entity.Leader.ToModel(),
                Led = entity.Led.ToModel(),
                Frequency = entity.Frequency,
                LastOneonone = entity.LastOneonone,
            };
            return model;
        }
    }
}