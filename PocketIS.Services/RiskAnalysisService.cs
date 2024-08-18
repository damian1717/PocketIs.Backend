using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly IRiskAnalysisRepository _riskAnalysisRepository;

        public RiskAnalysisService(IRiskAnalysisRepository riskAnalysisRepository)
        {
            _riskAnalysisRepository = riskAnalysisRepository;
        }
        public async Task<Guid> AddRiskAnalysAsync(RiskAnalys risk) 
            => await _riskAnalysisRepository.AddRiskAnalysAsync(risk);

        public async Task DeleteRiskAnalysAsync(Guid id) 
            => await _riskAnalysisRepository.DeleteRiskAnalysAsync(id);

        public async Task<RiskAnalys> GetRiskAnalysAsync(Guid id)
            => await _riskAnalysisRepository.GetRiskAnalysAsync(id);

        public async Task<RiskAnalys> GetRiskAnalysByProcessIdAsync(Guid processId)
            => await _riskAnalysisRepository.GetRiskAnalysByProcessIdAsync(processId);

        public async Task UpdateRiskAnalysAsync(RiskAnalys risk)
            => await _riskAnalysisRepository.UpdateRiskAnalysAsync(risk);
    }
}
