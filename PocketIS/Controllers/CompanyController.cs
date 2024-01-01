using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Domain;
using PocketIS.Models.Company;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("addcompany")]
        //[ValidateModel]
        public async Task<IActionResult> AddCompany(AddCompanyModel model)
        {
            var company = new Company
            {
                Name = model.Name,
                Code = model.Code,
                Director = model.Director,
                Nip = model.Nip,
                City = model.City,
                PostalCode = model.PostalCode,
                Street = model.Street,
                NumberBuilding = model.NumberBuilding,
                NumberApartment = model.NumberApartment,
                KnowHow = model.KnowHow,
                ItemsCompany = model.ItemsCompany,
                TechnologiesUsed = model.TechnologiesUsed,
                CommunicationSystem = model.CommunicationSystem,
                Strengths = model.Strengths,
                Weaknesses = model.Weaknesses,
                OpportunitiesForTheCompany = model.OpportunitiesForTheCompany,
                ThreatsToTheCompany = model.ThreatsToTheCompany,
                InsertedDate = DateTime.Now
            };

            await _companyService.AddCompanyAsync(company);
            return Ok();
        }

        [HttpPost]
        [Route("updatecompany")]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyModel model)
        {
            if (model?.Id is null) return BadRequest();

            var company = new Company
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Director = model.Director,
                Nip = model.Nip,
                City = model.City,
                PostalCode = model.PostalCode,
                Street = model.Street,
                NumberBuilding = model.NumberBuilding,
                NumberApartment = model.NumberApartment,
                KnowHow = model.KnowHow,
                ItemsCompany = model.ItemsCompany,
                TechnologiesUsed = model.TechnologiesUsed,
                CommunicationSystem = model.CommunicationSystem,
                Strengths = model.Strengths,
                Weaknesses = model.Weaknesses,
                OpportunitiesForTheCompany = model.OpportunitiesForTheCompany,
                ThreatsToTheCompany = model.ThreatsToTheCompany,
                InsertedDate = DateTime.Now
            };

            await _companyService.UpdateCompanyAsync(company);
            return Ok();
        }

        [HttpGet]
        [Route("getcompanies")]
        public async Task<IActionResult> Get() 
            => Ok(await _companyService.GetCompaniesAsync());

        [HttpGet]
        [Route("getcompany/{id}")]
        public async Task<IActionResult> Get(Guid id) 
            => Ok(await _companyService.GetCompanyByIdAsync(id));
    }
}
