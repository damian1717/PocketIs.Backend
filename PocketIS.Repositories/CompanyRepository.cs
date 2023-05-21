using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public CompanyRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCompanyAsync(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Company>> GetCompaniesAsync() => await _dbContext.Companies.ToListAsync();

        public async Task<Company> GetCompanyByIdAsync(Guid id) => await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
    }
}
