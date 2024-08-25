using PocketIS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketIS.Repositories.Interfaces
{
    public interface IRiskAnalysisRepository
    {
        Task<Guid> AddRiskAnalysAsync(RiskAnalys risk);
        Task UpdateRiskAnalysAsync(RiskAnalys risk);
        Task<RiskAnalys> GetRiskAnalysAsync(Guid id);
        Task<List<RiskAnalys>> GetRiskAnalysByProcessIdAndTypeAsync(Guid processId, int type);
        Task DeleteRiskAnalysAsync(Guid id);
    }
}
