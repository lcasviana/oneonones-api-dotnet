using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeMap
    {
        public static OneononeEntity ToEntity(this OneononeViewModel viewModel)
        {
            if (viewModel?.Leader == null || viewModel?.Led == null) return null;

            var entity = new OneononeEntity
            {
                Leader = viewModel.Leader.ToEntity(),
                Led = viewModel.Led.ToEntity(),
                Frequency = viewModel.Frequency,
            };

            return entity;
        }

        public static OneononeViewModel ToViewModel(this OneononeEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new OneononeViewModel
            {
                Id = entity.Id,
                Leader = entity.Leader.ToViewModel(),
                Led = entity.Led.ToViewModel(),
                Frequency = entity.Frequency,
            };

            return viewModel;
        }

        public static OneononeInputEntity ToEntity(this OneononeInputViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new OneononeInputEntity
            {
                LeaderId = viewModel.LeaderId,
                LedId = viewModel.LedId,
                Frequency = viewModel.Frequency,
            };

            return entity;
        }
    }
}