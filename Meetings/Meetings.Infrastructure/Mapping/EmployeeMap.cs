using Meetings.Domain.Entities;
using Meetings.Infrastructure.ViewModel;

namespace Meetings.Infrastructure.Mapping
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