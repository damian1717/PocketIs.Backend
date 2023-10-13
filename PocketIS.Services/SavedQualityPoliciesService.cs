using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class SavedQualityPoliciesService : ISavedQualityPoliciesService
    {
        private readonly ISavedQualityPoliciesRepository _savedQualityPoliciesRepository;
        public SavedQualityPoliciesService(ISavedQualityPoliciesRepository savedQualityPoliciesRepository)
        {
            _savedQualityPoliciesRepository = savedQualityPoliciesRepository;
        }
        public async Task AddSavedQualityPolicyAsync(List<SavedQualityPolicy> items) => await _savedQualityPoliciesRepository.AddSavedQualityPolicyAsync(items);

        public async Task<List<QualityPolicy>> GetSavedQualityPoliciesAsync(Guid companyId, int version) => await _savedQualityPoliciesRepository.GetSavedQualityPoliciesAsync(companyId, version);

        public async Task<int> GetLastVersionAsync() => await _savedQualityPoliciesRepository.GetLastVersionAsync();
    }
}
