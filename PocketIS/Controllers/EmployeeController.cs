using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.EmployeeForTraining;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("addemployee")]
        public async Task<IActionResult> AddEmployee(AddEmployee model)
        {
            var employee = new Employee
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                CompanyId = CompanyId,
                Level = model.Level,
                Position = model.Position,
                InsertedDate = DateTime.Now
            };

            await _employeeService.AddEmployeeAsync(employee);
            return Ok();
        }

        [HttpPost]
        [Route("updateemployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployee model)
        {
            if (model?.Id is null) return BadRequest();

            var employee = new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                LastName = model.LastName,
                Level = model.Level,
                Position = model.Position,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _employeeService.UpdateEmployeeAsync(employee);
            return Ok();
        }

        [HttpGet]
        [Route("getemployees")]
        public async Task<IActionResult> Get() 
            => Ok(await _employeeService.GetEmployeesAsync(CompanyId));

        [HttpGet]
        [Route("getemployee/{id}")]
        public async Task<IActionResult> Get(Guid id) 
            => Ok(await _employeeService.GetEmployeeByIdAsync(id));

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
