using System.Collections.Generic;
using System.Threading.Tasks;
using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IDashboardsService
    {
        Task<IList<DashboardEntity>> Obtain();
        Task<DashboardEntity> Obtain(string id);
    }
}