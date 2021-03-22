using Oneonones.Domain.Entities;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeModel ToModel(this EmployeeEntity entity)
        {
            if (entity == null) return null;

            var model = new EmployeeModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name,
            };

            return model;
        }

        public static EmployeeEntity ToEntity(this EmployeeModel model)
        {
            if (model == null) return null;

            var entity = new EmployeeEntity
            {
                Id = model.Id,
                Email = model.Email,
                Name = model.Name,
            };

            return entity;
        }
    }
}