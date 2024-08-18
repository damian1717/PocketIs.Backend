using PocketIS.Domain;
using System;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IRiskAnalysisService
    {
        Task<Guid> AddRiskAnalysAsync(RiskAnalys risk);
        Task UpdateRiskAnalysAsync(RiskAnalys risk);
        Task<RiskAnalys> GetRiskAnalysAsync(Guid id);
        Task<RiskAnalys> GetRiskAnalysByProcessIdAsync(Guid processId);
        Task DeleteRiskAnalysAsync(Guid id);
    }
}
