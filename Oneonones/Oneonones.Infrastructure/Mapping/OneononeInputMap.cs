using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeInputMap
    {
        public static OneononeInputEntity ToEntity(this OneononeInputViewModel model)
        {
            var entity = new OneononeInputEntity
            {
                LeaderEmail = model.LeaderEmail,
                LedEmail = model.LedEmail,
                Frequency = model.Frequency,
            };
            return entity;
        }
    }
}