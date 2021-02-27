using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeStatusMap
    {
        public static OneononeStatusViewModel ToViewModel(this OneononeStatusEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new OneononeStatusViewModel
            {
                NextOccurrence = entity.NextOccurrence,
                LastOccurrence = entity.LastOccurrence,
                IsLate = entity.IsLate,
            };

            return viewModel;
        }
    }
}