using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeViewModel ToViewModel(this EmployeeEntity entity)
        {
            var model = new EmployeeViewModel
            {
                Name = entity.Name,
                Email = entity.Email,
            };
            return model;
        }
    }
}