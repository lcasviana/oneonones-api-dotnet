using Oneonones.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oneonones.Service.Contracts
{
    public interface IDashboardsService
    {
        Task<IList<DashboardEntity>> ObtainAll();
        Task<DashboardEntity> ObtainByEmployee(string email);
    }
}