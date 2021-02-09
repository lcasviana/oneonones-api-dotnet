using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeInputMap
    {
        public static OneononeInputEntity ToEntity(this OneononeInputViewModel viewModel)
        {
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