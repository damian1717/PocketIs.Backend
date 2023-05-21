using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task AddCompanyAsync(Company company) => await _companyRepository.AddCompanyAsync(company);

        public async Task UpdateCompanyAsync(Company company) => await _companyRepository.UpdateCompanyAsync(company);

        public async Task<List<Company>> GetCompaniesAsync() => await _companyRepository.GetCompaniesAsync();

        public async Task<Company> GetCompanyByIdAsync(Guid id) => await _companyRepository.GetCompanyByIdAsync(id);
    }
}
