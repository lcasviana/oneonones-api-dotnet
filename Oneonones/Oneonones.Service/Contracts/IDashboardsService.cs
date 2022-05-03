using Oneonones.Domain.Entities;

namespace Oneonones.Service.Contracts
{
    public interface IDashboardsService
    {
        Task<IList<DashboardEntity>> Obtain();
        Task<DashboardEntity> Obtain(string id);
        Task<DashboardEntity> ObtainByEmail(string email);
    }
}