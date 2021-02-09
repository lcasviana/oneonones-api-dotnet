﻿using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeMap
    {
        public static OneononeViewModel ToViewModel(this OneononeEntity entity)
        {
            var viewModel = new OneononeViewModel
            {
                Leader = entity.Leader.ToViewModel(),
                Led = entity.Led.ToViewModel(),
                Frequency = entity.Frequency,
            };
            return viewModel;
        }
    }
}