using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface ISavedQualityPoliciesService
    {
        Task<List<QualityPolicy>> GetSavedQualityPoliciesAsync(Guid companyId, int version);
        Task AddSavedQualityPolicyAsync(List<SavedQualityPolicy> items);
        Task<int> GetLastVersionAsync();
    }
}
