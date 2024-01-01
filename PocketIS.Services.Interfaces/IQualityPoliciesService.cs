using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IQualityPoliciesService
    {
        Task<QualityPolicy> GetQualityPolicyAsync(Guid id);
        Task<List<QualityPolicy>> GetQualityPoliciesAsync();
        Task AddQualityPolicyAsync(QualityPolicy qualityPolicy);
        Task UpdateQualityPolicyAsync(QualityPolicy qualityPolicy);
    }
}
