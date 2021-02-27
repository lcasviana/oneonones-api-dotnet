using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeInputMap
    {
        public static OneononeInputEntity ToEntity(this OneononeInputViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new OneononeInputEntity
            {
                LeaderEmail = viewModel.LeaderEmail,
                LedEmail = viewModel.LedEmail,
                Frequency = viewModel.Frequency,
            };

            return entity;
        }
    }
}