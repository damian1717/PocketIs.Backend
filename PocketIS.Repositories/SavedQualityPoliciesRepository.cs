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
    public class SavedQualityPoliciesRepository : BaseRepository, ISavedQualityPoliciesRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public SavedQualityPoliciesRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            :base(userProvider)
        {
            _dbContext = dbContext;
        }

        public async Task<List<QualityPolicy>> GetSavedQualityPoliciesAsync(int version)
        {

            var query = from policy in _dbContext.QualityPolicies
                        where policy.SavedQualityPolicies.Any(c => c.Version == version && c.CompanyId == CompanyId)
                        select policy;

            return await query.ToListAsync();
        }

        public async Task<int> GetLastVersionAsync()
        {
            return await _dbContext.SavedQualityPolicies
                            .Where(x => x.CompanyId == CompanyId)
                            .MaxAsync(x => (int?)x.Version)
                            ?? 0;
        }

        public async Task AddSavedQualityPolicyAsync(List<SavedQualityPolicy> items)
        {
            foreach (var item in items)
            {
                item.InsertedDate = DateTime.Now;
                item.InsertedUserId = UserId;
                item.UpdatedDate = DateTime.Now;
                item.UpdatedUserId = UserId;
            }
            
            _dbContext.SavedQualityPolicies.AddRange(items);
            await _dbContext.SaveChangesAsync();
        }
    }
}
