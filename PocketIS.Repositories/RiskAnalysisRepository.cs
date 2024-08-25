using Microsoft.EntityFrameworkCore;
using PocketIS.Application.Common.Interfaces;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Repositories
{
    public class RiskAnalysisRepository : BaseRepository, IRiskAnalysisRepository
    {
        private readonly IApplicationDbContext _dbContext;
        public RiskAnalysisRepository(IUserProvider userProvider, IApplicationDbContext dbContext)
            : base(userProvider)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> AddRiskAnalysAsync(RiskAnalys risk)
        {
            risk.InsertedDate = DateTime.Now;
            risk.InsertedUserId = UserId;
            risk.UpdatedDate = DateTime.Now;
            risk.UpdatedUserId = UserId;

            await _dbContext.RiskAnalyses.AddAsync(risk);
            await _dbContext.SaveChangesAsync();

            return risk.Id;
        }

        public async Task DeleteRiskAnalysAsync(Guid id)
        {
            var risk = new RiskAnalys()
            {
                Id = id
            };

            _dbContext.RiskAnalyses.Remove(risk);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RiskAnalys> GetRiskAnalysAsync(Guid id)
            => await _dbContext.RiskAnalyses
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<RiskAnalys>> GetRiskAnalysByProcessIdAndTypeAsync(Guid processId, int type)
            => await _dbContext.RiskAnalyses
                .AsNoTracking()
                .Where(x => x.ProcessId == processId
                && x.Type == type)
                .ToListAsync();

        public async Task UpdateRiskAnalysAsync(RiskAnalys risk)
        {
            var currentRisk = _dbContext.RiskAnalyses.Find(risk.Id);

            if (currentRisk is not null)
            {
                currentRisk.RiskType = risk.RiskType;
                currentRisk.DefinedIssue = risk.DefinedIssue;
                currentRisk.PotentialCause = risk.PotentialCause;
                currentRisk.Degree = risk.Degree;
                currentRisk.Occurrence = risk.Occurrence;
                currentRisk.PreventiveAction = risk.PreventiveAction;
                currentRisk.PersonForSystemImplementation = risk.PersonForSystemImplementation;
                currentRisk.PlannedSystemImplementationDate = risk.PlannedSystemImplementationDate;
                currentRisk.RealSystemImplementationDate = risk.RealSystemImplementationDate;
                currentRisk.ImplementationStatus = risk.ImplementationStatus;
                currentRisk.SystemPerformance = risk.SystemPerformance;
                currentRisk.DateEffectivenessOfSystemOperation = risk.DateEffectivenessOfSystemOperation;
                currentRisk.OccurrenceAction = risk.OccurrenceAction;
                currentRisk.DegreeAction = risk.DegreeAction;
                currentRisk.EmergencyPlan = risk.EmergencyPlan;
                currentRisk.PersonForEmergencyPlan = risk.PersonForEmergencyPlan;
                currentRisk.AssessmentOfVerificationEffectiveness = risk.AssessmentOfVerificationEffectiveness;
                currentRisk.OwnerOfProcess = risk.OwnerOfProcess;
                currentRisk.ChangedFields = risk.ChangedFields;
                currentRisk.Version = risk.Version;
                currentRisk.CurrentUpdateId = risk.CurrentUpdateId;

                currentRisk.UpdatedDate = DateTime.Now;
                currentRisk.UpdatedUserId = UserId;

                _dbContext.RiskAnalyses.Update(currentRisk);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
