using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Training;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingController : BaseController
    {
        private readonly ITrainingService _trainingService;
        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        [Route("addtraining")]
        public async Task<IActionResult> AddProcess(AddTraining model)
        {
            var training = new Training
            {
                Name = model.Name,
                CompanyId = CompanyId,
                Level = model.Level,
                ForHowManyMonths = model.ForHowManyMonths,
                InsertedDate = DateTime.Now
            };

            await _trainingService.AddTrainingAsync(training);
            return Ok();
        }

        [HttpPost]
        [Route("updatetraining")]
        public async Task<IActionResult> UpdateProcess(UpdateTraining model)
        {
            if (model?.Id is null) return BadRequest();

            var training = new Training
            {
                Id = model.Id,
                Name = model.Name,
                Level = model.Level,
                ForHowManyMonths = model.ForHowManyMonths,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _trainingService.UpdateTrainingAsync(training);
            return Ok();
        }

        [HttpGet]
        [Route("gettrainings")]
        public async Task<IActionResult> Get() => Ok(await _trainingService.GetTrainingsAsync(CompanyId));

        [HttpGet]
        [Route("gettraining/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _trainingService.GetTrainingByIdAsync(id));

        [HttpDelete]
        [Route("deletetraining/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _trainingService.DeleteTrainingAsync(id);
            return Ok();
        }
    }
}
