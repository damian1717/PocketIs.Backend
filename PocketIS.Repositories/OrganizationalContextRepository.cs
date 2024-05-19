using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class OrganizationalContextRepository : BaseRepository, IOrganizationalContextRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public OrganizationalContextRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrganizationalContextAsync(OrganizationalContext organizationalContext)
        {
            organizationalContext.InsertedDate = DateTime.Now;
            organizationalContext.InsertedUserId = UserId;
            organizationalContext.UpdatedDate = DateTime.Now;
            organizationalContext.UpdatedUserId = UserId;

            await _dbContext.OrganizationalContexts.AddAsync(organizationalContext);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<OrganizationalContext> GetOrganizationalContextByUserIdAsync(Guid userId)
            => await _dbContext.OrganizationalContexts
                .FirstOrDefaultAsync(x => x.InsertedUserId == userId
                && x.CompanyId == CompanyId);

        public async Task UpdateOrganizationalContextAsync(OrganizationalContext organizationalContext)
        {
            var currentOrganizationalContext = _dbContext.OrganizationalContexts.Find(organizationalContext.Id);

            if (currentOrganizationalContext is not null)
            {
                currentOrganizationalContext.Industry = organizationalContext.Industry;
                currentOrganizationalContext.GoalAndStrategy = organizationalContext.GoalAndStrategy;
                currentOrganizationalContext.TechnologiesOwned = organizationalContext.TechnologiesOwned;
                currentOrganizationalContext.CommunicationSystem = organizationalContext.CommunicationSystem;
                currentOrganizationalContext.FactorsTechnologicalEnvironment = organizationalContext.FactorsTechnologicalEnvironment;
                currentOrganizationalContext.FactorsRelatedToValuesCulture = organizationalContext.FactorsRelatedToValuesCulture;
                currentOrganizationalContext.ExternalAndInternalEntities = organizationalContext.ExternalAndInternalEntities;
                currentOrganizationalContext.Strengths = organizationalContext.Strengths;
                currentOrganizationalContext.Weaknesses = organizationalContext.Weaknesses;
                currentOrganizationalContext.Chances = organizationalContext.Chances;
                currentOrganizationalContext.Threats = organizationalContext.Threats;
                currentOrganizationalContext.Norm = organizationalContext.Norm;
                currentOrganizationalContext.NumberNorm = organizationalContext.NumberNorm;
                currentOrganizationalContext.NormJustification = organizationalContext.NormJustification;

                currentOrganizationalContext.UpdatedDate = DateTime.Now;
                currentOrganizationalContext.UpdatedUserId = UserId;

                _dbContext.OrganizationalContexts.Update(currentOrganizationalContext);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
