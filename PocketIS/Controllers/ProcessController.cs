using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Process;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        private readonly IProcessService _processService;
        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        [HttpPost]
        [Route("addprocess")]
        public async Task<IActionResult> AddProcess(AddProcess model)
        {
            var process = new Process
            {
                Name = model.Name,
                CompanyId = CompanyId,
                IsBaseProcess = model.IsBaseProcess,
                InsertedDate = DateTime.Now
            };

            await _processService.AddProcessAsync(process);
            return Ok();
        }

        [HttpPost]
        [Route("updateprocess")]
        public async Task<IActionResult> UpdateProcess(UpdateProcess model)
        {
            if (model?.Id is null) return BadRequest();

            var process = new Process
            {
                Id = model.Id,
                Name = model.Name,
                IsBaseProcess = model.IsBaseProcess,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _processService.UpdateProcessAsync(process);
            return Ok();
        }

        [HttpGet]
        [Route("getprocesses")]
        public async Task<IActionResult> Get() => Ok(await _processService.GetProcessesAsync(CompanyId));

        [HttpGet]
        [Route("getbaseprocesses")]
        public async Task<IActionResult> GetBaseProcesses() => Ok(await _processService.GetBaseProcessesAsync(CompanyId));

        [HttpGet]
        [Route("getprocess/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _processService.GetProcessByIdAsync(id));
    }
}
