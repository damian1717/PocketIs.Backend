using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.SubProcess;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubProcessController : BaseController
    {
        private readonly ISubProcessService _subProcessService;
        public SubProcessController(ISubProcessService subProcessService)
        {
            _subProcessService = subProcessService;
        }

        [HttpPost]
        [Route("addsubprocess")]
        public async Task<IActionResult> AddSubProcess(AddSubProcess model)
        {
            var subProcess = new SubProcess
            {
                Name = model.Name,
                Description = model.Description,
                SubProcessType = model.SubProcessType,
                HtmlContent = model.HtmlContent,
                CompanyId = CompanyId,
                IsArchive = false,
                InsertedDate = DateTime.Now
            };

            await _subProcessService.AddSubProcessAsync(subProcess);
            return Ok();
        }

        [HttpPost]
        [Route("updatesubprocess")]
        public async Task<IActionResult> UpdateSubProcess(UpdateSubProcess model)
        {
            if (model?.Id is null) return BadRequest();

            var subProcess = new SubProcess
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                SubProcessType = model.SubProcessType,
                HtmlContent = model.HtmlContent,
                CompanyId = CompanyId,
                IsArchive = model.IsArchive
            };

            await _subProcessService.UpdateSubProcessAsync(subProcess);
            return Ok();
        }

        [HttpGet]
        [Route("getsubprocesses/{subProcessType}")]
        public async Task<IActionResult> Get(int subProcessType)
            => Ok(await _subProcessService.GetSubProcessesAsync(subProcessType));

        [HttpGet]
        [Route("getsubprocess/{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _subProcessService.GetSubProcessByIdAsync(id));

        [HttpDelete]
        [Route("deletesubprocess/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subProcessService.DeleteSubProcessAsync(id);
            return Ok();
        }
    }
}
