using Oneonones.Domain.Entities;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;
using System.Net;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class OneononesService : IOneononesService
    {
        private readonly IEmployeesRepository employeesRepository;
        private readonly IOneononesRepository oneononesRepository;

        public OneononesService(
            IEmployeesRepository employeesRepository,
            IOneononesRepository oneononesRepository)
        {
            this.employeesRepository = employeesRepository;
            this.oneononesRepository = oneononesRepository;
        }

        public async Task<OneononeEntity> Obtain(string leaderEmail, string ledEmail)
        {
            var (leader, led) = await ObtainEmployees(leaderEmail, ledEmail);

            var oneonone = await oneononesRepository.Obtain(leaderEmail, ledEmail)
                ?? throw new ApiException(HttpStatusCode.NotFound);

            oneonone.Leader = leader;
            oneonone.Led = led;

            return oneonone;
        }

        public async Task Insert(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await ObtainEmployees(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            var oneononeObtained = await oneononesRepository.Obtain(oneononeInput.LeaderEmail, oneononeInput.LedEmail);
            if (oneononeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict);

            var oneonone = new OneononeEntity
            {
                Leader = leader,
                Led = led,
                Frequency = oneononeInput.Frequency,
            };

            await oneononesRepository.Insert(oneonone);
        }

        public async Task Update(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await ObtainEmployees(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            var oneonone = await oneononesRepository.Obtain(oneononeInput.LeaderEmail, oneononeInput.LedEmail)
                ?? throw new ApiException(HttpStatusCode.NotFound);

            oneonone.Leader = leader;
            oneonone.Led = led;

            await oneononesRepository.Update(oneonone);
        }

        public async Task Delete(string leaderEmail, string ledEmail)
        {
            _ = await ObtainEmployees(leaderEmail, ledEmail);

            await oneononesRepository.Delete(leaderEmail, ledEmail);
        }

        private async Task<(EmployeeEntity, EmployeeEntity)> ObtainEmployees(string leaderEmail, string ledEmail)
        {
            if (string.IsNullOrWhiteSpace(leaderEmail)) throw new ApiException(HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(ledEmail)) throw new ApiException(HttpStatusCode.BadRequest);

            var leaderTask = employeesRepository.Obtain(leaderEmail);
            var ledTask = employeesRepository.Obtain(ledEmail);

            var leader = await leaderTask ?? throw new ApiException(HttpStatusCode.NotFound);
            var led = await ledTask ?? throw new ApiException(HttpStatusCode.NotFound);

            return (leader, led);
        }
    }
}