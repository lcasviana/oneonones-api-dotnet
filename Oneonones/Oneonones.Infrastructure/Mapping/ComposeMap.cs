using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;
using System.Linq;

namespace Oneonones.Infrastructure.Mapping
{
    public static class ComposeMap
    {
        public static OneononeComposeViewModel ToViewModel(this OneononeComposeEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new OneononeComposeViewModel
            {
                Oneonone = entity.Oneonone.ToViewModel(),
                Historical = entity.Historical.Select(HistoricalMap.ToViewModel).ToList(),
                Status = entity.Status.ToViewModel(),
            };

            return viewModel;
        }
    }
}