using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oneonones.Infrastructure.Mapping;
using Oneonones.Infrastructure.ViewModels;
using Oneonones.Service.Contracts;

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
        public async Task<IActionResult> Obtain([FromQuery] string email = null)
        {
            if (email == null)
            {
                var employeeEntityList = await employeesService.Obtain();
                var employeeViewModelList = employeeEntityList.Select(EmployeeMap.ToViewModel).ToList();
                return StatusCode((int)StatusCodes.Status200OK, employeeViewModelList);
            }
            else
            {
                var employeeEntity = await employeesService.ObtainByEmail(email);
                var employeeViewModel = employeeEntity.ToViewModel();
                return StatusCode((int)StatusCodes.Status200OK, employeeViewModel);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtainById([FromRoute] string id)
        {
            var employeeEntity = await employeesService.Obtain(id);
            var employeeViewModel = employeeEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status200OK, employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] EmployeeInputViewModel employeeInputViewModel)
        {
            var employeeInputEntity = employeeInputViewModel.ToEntity();
            var employeeEntity = await employeesService.Insert(employeeInputEntity);
            var employeeViewModel = employeeEntity.ToViewModel();
            return StatusCode((int)StatusCodes.Status201Created, employeeViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeViewModel employeeViewModel)
        {
            var employeeEntity = employeeViewModel.ToEntity();
            var employeeEntityUpdated = await employeesService.Update(employeeEntity);
            var employeeViewModelUpdated = employeeEntityUpdated.ToViewModel();
            return StatusCode((int)StatusCodes.Status202Accepted, employeeViewModelUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await employeesService.Delete(id);
            return StatusCode((int)StatusCodes.Status204NoContent);
        }
    }
}