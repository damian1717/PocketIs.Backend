using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IQualityPoliciesRepository
    {
        Task<QualityPolicy> GetQualityPolicyAsync(Guid id);
        Task<List<QualityPolicy>> GetQualityPoliciesAsync(Guid companyId);
        Task AddQualityPolicyAsync(QualityPolicy qualityPolicy);
        Task UpdateQualityPolicyAsync(QualityPolicy qualityPolicy);
    }
}
