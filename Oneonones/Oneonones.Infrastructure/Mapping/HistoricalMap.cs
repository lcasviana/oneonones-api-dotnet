using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class HistoricalMap
    {
        public static HistoricalEntity ToEntity(this HistoricalViewModel viewModel)
        {
            if (viewModel?.Leader == null || viewModel?.Led == null) return null;

            var entity = new HistoricalEntity
            {
                Id = viewModel.Id,
                Leader = viewModel.Leader.ToEntity(),
                Led = viewModel.Led.ToEntity(),
                Occurrence = viewModel.Occurrence,
                Commentary = viewModel.Commentary,
            };

            return entity;
        }

        public static HistoricalViewModel ToViewModel(this HistoricalEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new HistoricalViewModel
            {
                Id = entity.Id,
                Leader = entity.Leader.ToViewModel(),
                Led = entity.Led.ToViewModel(),
                Occurrence = entity.Occurrence,
                Commentary = entity.Commentary,
            };

            return viewModel;
        }

        public static HistoricalInputEntity ToEntity(this HistoricalInputViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new HistoricalInputEntity
            {
                LeaderId = viewModel.LeaderId,
                LedId = viewModel.LedId,
                Occurrence = viewModel.Occurrence,
                Commentary = viewModel.Commentary,
            };

            return entity;
        }
    }
}