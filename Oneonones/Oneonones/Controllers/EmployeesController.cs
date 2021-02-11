using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModel;
using Oneonones.Service.Contracts;
using System.Threading.Tasks;

namespace Oneonones.Controllers
{
    [ApiController]
    [Route("api/v1/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        [HttpGet]
        public async Task<IActionResult> Obtain([FromQuery] string email)
        {
            var oneononeEntity = await employeesService.Obtain(email);
            var oneononeModel = oneononeEntity.ToViewModel();
            return Ok(oneononeModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] EmployeeViewModel employeeViewModel)
        {
            var employeeEntity = employeeViewModel.ToEntity();
            await employeesService.Insert(employeeEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeViewModel employeeViewModel)
        {
            var employeeEntity = employeeViewModel.ToEntity();
            await employeesService.Update(employeeEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string email)
        {
            await employeesService.Delete(email);
            return NoContent();
        }
    }
}