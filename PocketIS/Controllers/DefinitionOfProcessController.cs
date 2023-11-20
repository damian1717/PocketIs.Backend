using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.DefinitionOfProcess;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DefinitionOfProcessController : BaseController
    {
        private readonly IDefinitionOfProcessService _definitionOfProcessService;
        public DefinitionOfProcessController(IDefinitionOfProcessService definitionOfProcessService)
        {
            _definitionOfProcessService = definitionOfProcessService;
        }

        [HttpPost]
        [Route("adddefinitionofprocess")]
        public async Task<IActionResult> AddDefinitionOfProcess(AddDefinitionOfProcess model)
        {
            var definitionOfProcess = new DefinitionOfProcess
            {
                Name = model.Name,
                Code = model.Code,
                IsBase = model.IsBase,
                InsertedDate = DateTime.Now,
                ProcessId = model.ProcessId
            };

            await _definitionOfProcessService.AddDefinitionOfProcessAsync(definitionOfProcess);
            return Ok();
        }

        [HttpPost]
        [Route("updatedefinitionofprocess")]
        public async Task<IActionResult> UpdateDefinitionOfProcess(UpdateDefinitionOfProcess model)
        {
            if (model?.Id is null) return BadRequest();

            var definitionOfProcess = new DefinitionOfProcess
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                IsBase = model.IsBase,
                ProcessId = model.ProcessId
            };

            await _definitionOfProcessService.UpdateDefinitionOfProcessAsync(definitionOfProcess);
            return Ok();
        }

        [HttpGet]
        [Route("getdefinitionofprocess/{processId}")]
        public async Task<IActionResult> GetDefinitionOfProcess(Guid processId) 
            => Ok(await _definitionOfProcessService.GetDefinitionOfProcessAsync(processId));

        [HttpGet]
        [Route("getbasedefinitionofprocess/{processId}")]
        public async Task<IActionResult> GetBaseDefinitionOfProcess(Guid processId) 
            => Ok(await _definitionOfProcessService.GetBaseDefinitionOfProcessAsync(processId));

        [HttpGet]
        [Route("getdefinitionofprocessbyid/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _definitionOfProcessService.GetDefinitionOfProcessByIdAsync(id));

        [HttpDelete]
        [Route("deletedefinitionofprocess/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _definitionOfProcessService.DeleteDefinitionOfProcessAsync(id);
            return Ok();
        }
    }
}
