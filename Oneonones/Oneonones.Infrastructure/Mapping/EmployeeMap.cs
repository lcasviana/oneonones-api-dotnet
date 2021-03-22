using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeEntity ToEntity(this EmployeeViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new EmployeeEntity
            {
                Id = viewModel.Id,
                Email = viewModel.Email,
                Name = viewModel.Name,
            };

            return entity;
        }

        public static EmployeeViewModel ToViewModel(this EmployeeEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new EmployeeViewModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
            };

            return viewModel;
        }

        public static EmployeeInputEntity ToEntity(this EmployeeInputViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new EmployeeInputEntity
            {
                Email = viewModel.Email,
                Name = viewModel.Name,
            };

            return entity;
        }
    }
}