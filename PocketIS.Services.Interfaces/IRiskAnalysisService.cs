using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IRiskAnalysisService
    {
        Task<Guid> AddRiskAnalysAsync(RiskAnalys risk);
        Task UpdateRiskAnalysAsync(RiskAnalys risk);
        Task<RiskAnalys> GetRiskAnalysAsync(Guid id);
        Task<List<RiskAnalysInfo>> GetRiskAnalysByProcessIdAndTypeAsync(Guid processId, int type);
        Task DeleteRiskAnalysAsync(Guid id);
    }
}
