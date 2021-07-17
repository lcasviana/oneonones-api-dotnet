using Oneonones.Domain.Entities;
using Oneonones.Persistence.Models;

namespace Oneonones.Persistence.Mapping
{
    public static class AccountMap
    {
        public static AccountModel ToModel(this AccountEntity entity)
        {
            if (entity == null) return null;

            var model = new AccountModel
            {
                EmployeeId = entity.EmployeeId,
                Password = entity.Password,
                Admin = entity.Admin,
            };

            return model;
        }

        public static AccountEntity ToEntity(this AccountModel model)
        {
            if (model == null) return null;

            var entity = new AccountEntity
            {
                EmployeeId = model.EmployeeId,
                Password = model.Password,
                Admin = model.Admin,
            };

            return entity;
        }
    }
}