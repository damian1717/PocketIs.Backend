using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class SavedQualityPoliciesRepository : ISavedQualityPoliciesRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public SavedQualityPoliciesRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<QualityPolicy>> GetSavedQualityPoliciesAsync(Guid companyId, int version)
        {

            var query = from policy in _dbContext.QualityPolicies
                        where policy.SavedQualityPolicies.Any(c => c.Version == version && c.CompanyId == companyId)
                        select policy;

            return await query.ToListAsync();
        }

        public async Task<int> GetLastVersionAsync(Guid companyId)
        {
            return await _dbContext.SavedQualityPolicies
                            .Where(x => x.CompanyId == companyId)
                            .MaxAsync(x => (int?)x.Version)
                            ?? 0;
        }

        public async Task AddSavedQualityPolicyAsync(List<SavedQualityPolicy> items)
        {
            _dbContext.SavedQualityPolicies.AddRange(items);
            await _dbContext.SaveChangesAsync();
        }
    }
}
