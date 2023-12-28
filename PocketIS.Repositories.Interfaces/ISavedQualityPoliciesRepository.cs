using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface ISavedQualityPoliciesRepository
    {
        Task<List<QualityPolicy>> GetSavedQualityPoliciesAsync(Guid companyId, int version);
        Task AddSavedQualityPolicyAsync(List<SavedQualityPolicy> items);
        Task<int> GetLastVersionAsync(Guid companyId);
    }
}
