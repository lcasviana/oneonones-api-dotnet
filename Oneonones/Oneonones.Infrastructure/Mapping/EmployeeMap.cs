using Oneonones.Domain.Entities;
using Oneonones.Infrastructure.ViewModel;

namespace Oneonones.Infrastructure.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeModel ToModel(this EmployeeEntity entity)
        {
            var model = new EmployeeModel
            {
                Name = entity.Name,
                Email = entity.Email,
            };
            return model;
        }
    }
}