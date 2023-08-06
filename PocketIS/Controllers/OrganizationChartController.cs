using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.OrganizationChart;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationChartController : BaseController
    { 
        private readonly IOrganizationChartService _organizationChartService;
        public OrganizationChartController(IOrganizationChartService organizationChartService)
        {
            _organizationChartService = organizationChartService;
        }

        [HttpPost]
        [Route("AddOrganizationChartPerson")]
        public async Task<IActionResult> AddOrganizationChartPerson(AddOrganizationChartPersonModel model)
        {
            var organizationChartPerson = new OrganizationChartPerson
            {
                Name = model.Name,
                LastName = model.LastName,
                Position = model.Position,
                BelowPersonId = model.BelowPersonId,
                InsertedDate = DateTime.Now,
                Level = model.Level
            };

            await _organizationChartService.AddOrganizationChartPersonAsync(organizationChartPerson);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateOrganizationChartPerson")]
        public async Task<IActionResult> UpdateOrganizationChartPerson(UpdateOrganizationChartPersonModel model)
        {
            if (model?.Id is null) return BadRequest();

            var organizationChartPerson = new OrganizationChartPerson
            {
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                Position = model.Position,
                BelowPersonId= model.BelowPersonId,
                InsertedDate = DateTime.Now,
                Level = model.Level
            };

            await _organizationChartService.UpdateOrganizationChartPersonAsync(organizationChartPerson);
            return Ok();
        }

        [HttpGet]
        [Route("GetPersonsListOrganizationChart")]
        public async Task<IActionResult> Get() => Ok(await _organizationChartService.GetListOfPersonsAsync());

        [HttpGet]
        [Route("GetPersonOrganizationChart/{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _organizationChartService.GetListOfPersonAsync(id));

        [HttpDelete]
        [Route("DeleteOrganizationChartPerson/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _organizationChartService.DeleteOrganizationChartPersonAsync(id);
            return Ok();
        }
    }
}
