using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class QualityPoliciesService : IQualityPoliciesService
    {
        private readonly IQualityPoliciesRepository _qualityPoliciesRepository;

        public QualityPoliciesService(IQualityPoliciesRepository qualityPoliciesRepository)
        {
            _qualityPoliciesRepository = qualityPoliciesRepository;
        }

        public async Task<QualityPolicy> GetQualityPolicyAsync(Guid id) => await _qualityPoliciesRepository.GetQualityPolicyAsync(id);

        public async Task<List<QualityPolicy>> GetQualityPoliciesAsync() => await _qualityPoliciesRepository.GetQualityPoliciesAsync();

        public async Task AddQualityPolicyAsync(QualityPolicy qualityPolicy) => await _qualityPoliciesRepository.AddQualityPolicyAsync(qualityPolicy);

        public async Task UpdateQualityPolicyAsync(QualityPolicy qualityPolicy) => await _qualityPoliciesRepository.UpdateQualityPolicyAsync(qualityPolicy);
    }
}
