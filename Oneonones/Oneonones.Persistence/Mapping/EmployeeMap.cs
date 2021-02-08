using Oneonones.Domain.Entities;
using Oneonones.Persistence.Models;

namespace Employees.Persistence.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeModel ToModel(this EmployeeEntity entity)
        {
            var model = new EmployeeModel
            {
                Email = entity.Email,
                Name = entity.Name,
            };
            return model;
        }

        public static EmployeeEntity ToEntity(this EmployeeModel model)
        {
            var entity = new EmployeeEntity
            {
                Email = model.Email,
                Name = model.Name,
            };
            return entity;
        }
    }
}