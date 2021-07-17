using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Domain.Extensions;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;

namespace Oneonones.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login, [FromQuery] bool encrypted)
        {
            if (!encrypted) login.Password = HashExtension.Digest(login.Password);
            var userEntity = await accountsService.Login(login.Email, login.Password);
            var userViewModel = userEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status202Accepted, userViewModel);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> ObtainById([FromRoute] string employeeId)
        {
            var accountEntity = await accountsService.Obtain(employeeId);
            var accountViewModel = accountEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, accountViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AccountViewModel accountViewModel, [FromQuery] bool encrypted)
        {
            if (!encrypted) accountViewModel.Password = HashExtension.Digest(accountViewModel.Password);
            var accountEntity = accountViewModel.ToEntity();
            var accountEntityInserted = await accountsService.Insert(accountEntity);
            var accountViewModelInserted = accountEntityInserted.ToViewModel();
            return StatusCode((int)StatusCodes.Status201Created, accountViewModelInserted);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AccountViewModel accountViewModel, [FromQuery] bool encrypted)
        {
            if (!encrypted) accountViewModel.Password = HashExtension.Digest(accountViewModel.Password);
            var accountEntity = accountViewModel.ToEntity();
            var accountEntityUpdated = await accountsService.Update(accountEntity);
            var accountViewModelUpdated = accountEntityUpdated.ToViewModel();
            return StatusCode((int)StatusCodes.Status202Accepted, accountViewModelUpdated);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> Delete([FromRoute] string employeeId)
        {
            await accountsService.Delete(employeeId);
            return StatusCode((int)StatusCodes.Status204NoContent);
        }
    }
}