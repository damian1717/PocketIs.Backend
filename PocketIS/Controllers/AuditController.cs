using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Audit;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : BaseController
    {
        private readonly IAuditService _auditService;
        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost]
        [Route("addaudit")]
        public async Task<IActionResult> AddAudit(AddAudit model)
        {
            var audit = new Audit
            {
                Name = model.Name,
                Type = model.Type,
                Status = model.Status,
                AuditDate = model.AuditDate,
                Auditor = model.Auditor,
                CompanyId = CompanyId
            };

            await _auditService.AddAuditAsync(audit);
            return Ok();
        }

        [HttpPost]
        [Route("updateaudit")]
        public async Task<IActionResult> UpdateAudit(UpdateAudit model)
        {
            if (model?.Id is null) return BadRequest();

            var audit = new Audit
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Status = model.Status,
                AuditDate = model.AuditDate,
                Auditor = model.Auditor
            };

            await _auditService.UpdateAuditAsync(audit);
            return Ok();
        }

        [HttpGet]
        [Route("getaudits")]
        public async Task<IActionResult> Get()
            => Ok(await _auditService.GetAuditsAsync());

        [HttpGet]
        [Route("getaudit/{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _auditService.GetAuditByIdAsync(id));

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _auditService.DeleteAuditAsync(id);
            return Ok();
        }
    }
}
