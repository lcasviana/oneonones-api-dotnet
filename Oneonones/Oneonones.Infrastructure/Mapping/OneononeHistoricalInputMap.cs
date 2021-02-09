using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeHistoricalInputMap
    {
        public static OneononeHistoricalInputEntity ToEntity(this OneononeHistoricalInputViewModel viewModel)
        {
            var entity = new OneononeHistoricalInputEntity
            {
                LeaderEmail = viewModel.LeaderEmail,
                LedEmail = viewModel.LedEmail,
                Occurrence = viewModel.Occurrence,
                Commentary = viewModel.Commentary,
            };
            return entity;
        }
    }
}