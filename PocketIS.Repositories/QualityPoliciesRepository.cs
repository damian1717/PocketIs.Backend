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
    public class QualityPoliciesRepository : BaseRepository, IQualityPoliciesRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public QualityPoliciesRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            :base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task<QualityPolicy> GetQualityPolicyAsync(Guid id) 
            => await _dbContext.QualityPolicies
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<QualityPolicy>> GetQualityPoliciesAsync() 
            => await _dbContext.QualityPolicies
                .Where(x => x.CompanyId == CompanyId)
                .ToListAsync();
        public async Task AddQualityPolicyAsync(QualityPolicy qualityPolicy)
        {
            await _dbContext.QualityPolicies.AddAsync(qualityPolicy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateQualityPolicyAsync(QualityPolicy qualityPolicy)
        {
            var currentQualityPolicy = _dbContext.QualityPolicies.Find(qualityPolicy.Id);

            if (currentQualityPolicy is not null)
            {
                currentQualityPolicy.IsInternal = qualityPolicy.IsInternal;
                currentQualityPolicy.IsExternal = qualityPolicy.IsExternal;
                currentQualityPolicy.Name = qualityPolicy.Name;

                _dbContext.QualityPolicies.Update(currentQualityPolicy);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
