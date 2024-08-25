using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.RiskAnalys;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RiskAnalysisController : ControllerBase
    {
        private readonly IRiskAnalysisService _riskAnalysisService;
        public RiskAnalysisController(IRiskAnalysisService riskAnalysisService)
        {
            _riskAnalysisService = riskAnalysisService;
        }

        [HttpPost]
        [Route("addriskanalys")]
        public async Task<IActionResult> AddRiskAnalys(AddRiskAnalys model)
        {
            var risk = new RiskAnalys
            {
                ProcessId = model.ProcessId,
                Type = model.Type,
                RiskType = model.RiskType,
                DefinedIssue = model.DefinedIssue,
                PotentialCause = model.PotentialCause,
                Degree = model.Degree,
                Occurrence = model.Occurrence,
                OwnerOfProcess = model.OwnerOfProcess,
                CurrentUpdateId = model.CurrentUpdateId,
                Version = 1,
                ChangedFields = "add",
                InsertedDate = DateTime.Now
            };

            return Ok(await _riskAnalysisService.AddRiskAnalysAsync(risk));
        }

        [HttpPost]
        [Route("updateriskanalys")]
        public async Task<IActionResult> UpdateRiskAnalys(UpdateRiskAnalys model)
        {
            if (model?.Id is null) return BadRequest();

            var risk = new RiskAnalys
            {
                Id = model.Id,
                ProcessId = model.ProcessId,
                Type = model.Type,
                RiskType = model.RiskType,
                DefinedIssue = model.DefinedIssue,
                PotentialCause = model.PotentialCause,
                Degree = model.Degree,
                Occurrence = model.Occurrence,
                InsertedDate = DateTime.Now,
                PreventiveAction = model.PreventiveAction,
                PersonForSystemImplementation = model.PersonForSystemImplementation,
                PlannedSystemImplementationDate = model.PlannedSystemImplementationDate,
                RealSystemImplementationDate = model.RealSystemImplementationDate,
                ImplementationStatus = model.ImplementationStatus,
                SystemPerformance = model.SystemPerformance,
                DateEffectivenessOfSystemOperation = model.DateEffectivenessOfSystemOperation,
                DegreeAction = model.DegreeAction,
                OccurrenceAction = model.OccurrenceAction,
                EmergencyPlan = model.EmergencyPlan,
                PersonForEmergencyPlan = model.PersonForEmergencyPlan,
                AssessmentOfVerificationEffectiveness = model.AssessmentOfVerificationEffectiveness,
                OwnerOfProcess = model.OwnerOfProcess,
                ChangedFields = model.ChangedFields,
                CurrentUpdateId = model.CurrentUpdateId
            };

            await _riskAnalysisService.UpdateRiskAnalysAsync(risk);
            return Ok();
        }

        [HttpGet]
        [Route("getriskanalysbyprocessidandtype/{type}/{processId}")]
        public async Task<IActionResult> GetRiskAnalysByProcessId(Guid processId, int type)
            => Ok(await _riskAnalysisService.GetRiskAnalysByProcessIdAndTypeAsync(processId, type));

        [HttpGet]
        [Route("getriskanalys/{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _riskAnalysisService.GetRiskAnalysAsync(id));

        [HttpDelete]
        [Route("deleteriskanalys/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _riskAnalysisService.DeleteRiskAnalysAsync(id);
            return Ok();
        }
    }
}
