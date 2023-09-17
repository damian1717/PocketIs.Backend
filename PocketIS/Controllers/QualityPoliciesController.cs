using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.QualityPolicy;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QualityPoliciesController : BaseController
    {
        private readonly IQualityPoliciesService _qualityPoliciesService;
        public QualityPoliciesController(IQualityPoliciesService qualityPoliciesService)
        {
            _qualityPoliciesService = qualityPoliciesService;
        }

        [HttpPost]
        [Route("addqualitypolicy")]
        public async Task<IActionResult> AddQualityPolicy(AddQualityPolicyModel model)
        {
            var qualityPolicy = new QualityPolicy
            {
                Name = model.Name,
                IsExternal = model.IsExternal,
                IsInternal = model.IsInternal,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _qualityPoliciesService.AddQualityPolicyAsync(qualityPolicy);
            return Ok();
        }

        [HttpPost]
        [Route("updatequalitypolicy")]
        public async Task<IActionResult> UpdateQualityPolicy(UpdateQualityPolicyModel model)
        {
            if (model?.Id is null) return BadRequest();

            var qualityPolicy = new QualityPolicy
            {
                Id = model.Id,
                Name = model.Name,
                IsExternal = model.IsExternal,
                IsInternal = model.IsInternal,
                InsertedDate = DateTime.Now
            };

            await _qualityPoliciesService.UpdateQualityPolicyAsync(qualityPolicy);
            return Ok();
        }

        [HttpGet]
        [Route("getqualitypolicies")]
        public async Task<IActionResult> Get() => Ok(await _qualityPoliciesService.GetQualityPoliciesAsync(CompanyId));

        [HttpGet]
        [Route("getqualitypolicy/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _qualityPoliciesService.GetQualityPolicyAsync(id));
    }
}
