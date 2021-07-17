using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModels;

namespace Oneonones.Infrastructure.Mapping
{
    public static class AccountMap
    {
        public static AccountEntity ToEntity(this AccountViewModel viewModel)
        {
            if (viewModel == null) return null;

            var entity = new AccountEntity
            {
                EmployeeId = viewModel.EmployeeId,
                Password = viewModel.Password,
                Admin = viewModel.Admin,
            };

            return entity;
        }

        public static AccountViewModel ToViewModel(this AccountEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new AccountViewModel
            {
                EmployeeId = entity.EmployeeId,
                Password = "*********",
                Admin = entity.Admin,
            };

            return viewModel;
        }

        public static UserViewModel ToViewModel(this UserEntity entity)
        {
            if (entity == null) return null;

            var viewModel = new UserViewModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
                Admin = entity.Admin,
            };

            return viewModel;
        }
    }
}