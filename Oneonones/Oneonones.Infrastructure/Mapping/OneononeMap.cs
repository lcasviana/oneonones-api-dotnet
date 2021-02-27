﻿using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class OneononeMap
    {
        public static OneononeViewModel ToViewModel(this OneononeEntity entity)
        {
            if (entity == null) return null;

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