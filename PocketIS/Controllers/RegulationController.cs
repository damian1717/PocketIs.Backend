using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Regulation;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegulationController : BaseController
    {
        private readonly IRegulationService _regulationService;
        public RegulationController(IRegulationService regulationService)
        {
            _regulationService = regulationService;
        }

        [HttpPost]
        [Route("addregulation")]
        public async Task<IActionResult> AddRegulation(AddRegulationModel model)
        {
            var regulation = new Regulation
            {
                Name = model.Name,
                Link = model.Link,
                Description = model.Description,
                CompanyId = CompanyId,
                InsertedDate = DateTime.Now
            };

            await _regulationService.AddRegulationAsync(regulation);
            return Ok();
        }

        [HttpPost]
        [Route("updateregulation")]
        public async Task<IActionResult> UpdateRegulation(UpdateRegulationModel model)
        {
            if (model?.Id is null) return BadRequest();

            var regulation = new Regulation
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Description = model.Description,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

            await _regulationService.UpdateRegulationAsync(regulation);
            return Ok();
        }

        [HttpGet]
        [Route("getregulations")]
        public async Task<IActionResult> Get() 
            => Ok(await _regulationService.GetRegulationsAsync());

        [HttpGet]
        [Route("getregulation/{id}")]
        public async Task<IActionResult> Get(Guid id) 
            => Ok(await _regulationService.GetRegulationByIdAsync(id));
    }
}
