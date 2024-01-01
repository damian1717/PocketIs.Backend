using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.EmployeeTraining;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTrainingController : BaseController
    {
        private readonly IEmployeeTrainingService _employeeTrainingService;
        public EmployeeTrainingController(IEmployeeTrainingService employeeTrainingService)
        {
            _employeeTrainingService = employeeTrainingService;
        }

        [HttpPost]
        [Route("addemployeetrainings")]
        public async Task<IActionResult> AddEmployeeTraining(AddEmployeeTraining model)
        {
            var employeeTraining = new EmployeeTraining
            {
                TrainingId = model.TrainingId,
                Required = model.Required,
                TrainingDate = model.TrainingDate,
                SkillLevel = model.SkillLevel,
                EmployeeId = model.EmployeeId,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _employeeTrainingService.AddEmployeeTrainingAsync(employeeTraining);
            return Ok();
        }

        [HttpPost]
        [Route("updateemployeetrainings")]
        public async Task<IActionResult> UpdateEmployeeTraining(UpdateEmployeeTraining model)
        {
            if (model?.Id is null) return BadRequest();

            var employeeTraining = new EmployeeTraining
            {
                Id = model.Id,
                TrainingId = model.TrainingId,
                Required = model.Required,
                TrainingDate = model.TrainingDate,
                SkillLevel = model.SkillLevel,
                EmployeeId = model.EmployeeId,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _employeeTrainingService.UpdateEmployeeTrainingAsync(employeeTraining);
            return Ok();
        }

        [HttpGet]
        [Route("getemployeetrainings/{employeeId}/{level}")]
        public async Task<IActionResult> GetEmployeeTrainings(Guid employeeId, int level)
            => Ok(await _employeeTrainingService.GetEmployeeTrainingsAsync(CompanyId, employeeId, level));

        [HttpGet]
        [Route("getemployeetrainingbyid/{id}")]
        public async Task<IActionResult> GetEmployeeTrainingById(Guid id)
            => Ok(await _employeeTrainingService.GetEmployeeTrainingByIdAsync(id));
    }
}
