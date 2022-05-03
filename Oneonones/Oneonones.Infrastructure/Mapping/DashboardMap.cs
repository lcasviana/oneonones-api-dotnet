using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class DashboardMap
    {
        public static DashboardViewModel ToViewModel(this DashboardEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new DashboardViewModel
            {
                Employee = entity.Employee.ToViewModel(),
                Oneonones = entity.Oneonones.Select(ComposeMap.ToViewModel).ToList(),
            };

            return viewModel;
        }
    }
}