using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModel;
using Oneonones.Service.Contracts;
using System.Linq;
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
        public async Task<IActionResult> ObtainAll()
        {
            var employeeEntityList = await employeesService.ObtainAll();
            var employeeViewModelList = employeeEntityList.Select(EmployeeMap.ToViewModel).ToList();
            return Ok(employeeViewModelList);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Obtain([FromRoute] string email)
        {
            var employeeEntity = await employeesService.Obtain(email);
            var employeeViewModel = employeeEntity.ToViewModel();
            return Ok(employeeViewModel);
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

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete([FromRoute] string email)
        {
            await employeesService.Delete(email);
            return NoContent();
        }
    }
}