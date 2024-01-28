using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public CompanyRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddCompanyAsync(Company company)
        {
            company.InsertedDate = DateTime.Now;
            company.InsertedUserId = UserId;
            company.UpdatedDate = DateTime.Now;
            company.UpdatedUserId = UserId;

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();

            var currentCompany = _dbContext.Companies.Find(company.Id);

            if (currentCompany is not null)
            {
                currentCompany.Name = company.Name;
                currentCompany.Director = company.Director;
                currentCompany.Nip = company.Nip;
                currentCompany.City = company.City;
                currentCompany.PostalCode = company.PostalCode;
                currentCompany.Street = company.Street;
                currentCompany.NumberBuilding = company.NumberBuilding;
                currentCompany.NumberApartment = company.NumberApartment;
                currentCompany.KnowHow = company.KnowHow;
                currentCompany.ItemsCompany = company.ItemsCompany;
                currentCompany.TechnologiesUsed = company.TechnologiesUsed;
                currentCompany.CommunicationSystem = company.CommunicationSystem;
                currentCompany.Strengths = company.Strengths;
                currentCompany.Weaknesses = company.Weaknesses;
                currentCompany.OpportunitiesForTheCompany = company.OpportunitiesForTheCompany;
                currentCompany.ThreatsToTheCompany = company.ThreatsToTheCompany;
                currentCompany.UpdatedDate = DateTime.Now;
                currentCompany.UpdatedUserId = UserId;

                _dbContext.Companies.Update(currentCompany);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Company>> GetCompaniesAsync() => await _dbContext.Companies.ToListAsync();

        public async Task<Company> GetCompanyByIdAsync(Guid id) => await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Company> GetCompanyByCodeAsync(string code) => await _dbContext.Companies.FirstOrDefaultAsync(x => x.Code.ToLower() == code.ToLower());
    }
}
