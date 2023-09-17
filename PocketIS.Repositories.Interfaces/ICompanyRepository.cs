using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddCompanyAsync(Company company);
        Task UpdateCompanyAsync(Company company);
        Task<List<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(Guid id);
        Task<Company> GetCompanyByCodeAsync(string code);
    }
}
