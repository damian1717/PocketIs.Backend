using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.OrganizationalContext;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationalContextController : BaseController
    {
        private readonly IOrganizationalContextService _organizationalContextService;
        public OrganizationalContextController(IOrganizationalContextService organizationalContextService)
        {
            _organizationalContextService = organizationalContextService;
        }

        [HttpPost]
        [Route("addorganizationalcontext")]
        public async Task<IActionResult> AddDefinitionOfProcess(AddOrganizationalContext model)
        {
            var organizationalContext = new OrganizationalContext
            {
                CompanyId = CompanyId,
                Industry = model.Industry,
                GoalAndStrategy = model.GoalAndStrategy,
                TechnologiesOwned = model.TechnologiesOwned,
                CommunicationSystem = model.CommunicationSystem,
                FactorsTechnologicalEnvironment = model.FactorsTechnologicalEnvironment,
                FactorsRelatedToValuesCulture = model.FactorsRelatedToValuesCulture,
                ExternalAndInternalEntities = model.ExternalAndInternalEntities,
                Strengths = model.Strengths,
                Weaknesses = model.Weaknesses,
                Chances = model.Chances,
                Threats = model.Threats,
                Norm = model.Norm,
                NumberNorm = model.NumberNorm,
                NormJustification = model.NormJustification
            };

            await _organizationalContextService.AddOrganizationalContextAsync(organizationalContext);
            return Ok();
        }

        [HttpPost]
        [Route("updatedeorganizationalcontext")]
        public async Task<IActionResult> UpdateDefinitionOfProcess(UpdateOrganizationalContext model)
        {
            if (model?.Id is null) return BadRequest();

            var organizationalContext = new OrganizationalContext
            {
                Id = model.Id,
                Industry = model.Industry,
                GoalAndStrategy = model.GoalAndStrategy,
                TechnologiesOwned = model.TechnologiesOwned,
                CommunicationSystem = model.CommunicationSystem,
                FactorsTechnologicalEnvironment = model.FactorsTechnologicalEnvironment,
                FactorsRelatedToValuesCulture = model.FactorsRelatedToValuesCulture,
                ExternalAndInternalEntities = model.ExternalAndInternalEntities,
                Strengths = model.Strengths,
                Weaknesses = model.Weaknesses,
                Chances = model.Chances,
                Threats = model.Threats,
                Norm = model.Norm,
                NumberNorm = model.NumberNorm,
                NormJustification = model.NormJustification
            };

            await _organizationalContextService.UpdateOrganizationalContextAsync(organizationalContext);
            return Ok();
        }

        [HttpGet]
        [Route("getorganizationalcontextbyuserid/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _organizationalContextService.GetOrganizationalContextByUserIdAsync(UserId));
    }
}
