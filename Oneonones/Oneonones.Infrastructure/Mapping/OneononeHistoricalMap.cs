using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeHistoricalMap
    {
        public static OneononeHistoricalViewModel ToViewModel(this OneononeHistoricalEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new OneononeHistoricalViewModel
            {
                Leader = entity.Leader.ToViewModel(),
                Led = entity.Led.ToViewModel(),
                Occurrence = entity.Occurrence,
                Commentary = entity.Commentary,
            };

            return viewModel;
        }
    }
}