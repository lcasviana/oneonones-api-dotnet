using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;
using System.Linq;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeComposeMap
    {
        public static OneononeComposeViewModel ToViewModel(this OneononeComposeEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new OneononeComposeViewModel
            {
                Oneonone = entity.Oneonone.ToViewModel(),
                Historical = entity.Historical.Select(OneononeHistoricalMap.ToViewModel).ToList(),
                Status = entity.Status.ToViewModel(),
            };

            return viewModel;
        }
    }
}