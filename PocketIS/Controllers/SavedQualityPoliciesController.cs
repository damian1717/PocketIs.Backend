using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.SavedQualityPolicy;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SavedQualityPoliciesController : BaseController
    {
        private readonly ISavedQualityPoliciesService _savedQualityPoliciesService;
        public SavedQualityPoliciesController(ISavedQualityPoliciesService savedQualityPoliciesService)
        {
            _savedQualityPoliciesService = savedQualityPoliciesService;
        }

        [HttpGet]
        [Route("getlastsavedqualitypolicy")]
        public async Task<IActionResult> Get()
        {
            var lastVersion = await _savedQualityPoliciesService.GetLastVersionAsync();

            if (lastVersion.Equals(0)) return Ok();

            return Ok(await _savedQualityPoliciesService.GetSavedQualityPoliciesAsync(lastVersion));
        }

        [HttpPost]
        [Route("addsavedqualitypolicy")]
        public async Task<IActionResult> AddSavedQualityPolicy(List<AddSavedQualityPolicyModel> model)
        {
            if (model is null || model.Count <= 0) return BadRequest();

            var lastVersion = await _savedQualityPoliciesService.GetLastVersionAsync();

            var savedItems = new List<SavedQualityPolicy>();
            foreach(var item in model)
            {
                var savedQualityPolicy = new SavedQualityPolicy
                {
                    QualityPolicyId = item.QualityPolicyId,
                    InsertedDate = DateTime.Now,
                    CompanyId = CompanyId,
                    Version = lastVersion + 1
                };

                savedItems.Add(savedQualityPolicy);
            }
            

            await _savedQualityPoliciesService.AddSavedQualityPolicyAsync(savedItems);
            return Ok();
        }
    }
}
