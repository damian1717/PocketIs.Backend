using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly IRiskAnalysisRepository _riskAnalysisRepository;
        private readonly IUserService _userService;
        public RiskAnalysisService(IRiskAnalysisRepository riskAnalysisRepository, IUserService userService)
        {
            _riskAnalysisRepository = riskAnalysisRepository;
            _userService = userService;
        }
        public async Task<Guid> AddRiskAnalysAsync(RiskAnalys risk) 
            => await _riskAnalysisRepository.AddRiskAnalysAsync(risk);

        public async Task DeleteRiskAnalysAsync(Guid id) 
            => await _riskAnalysisRepository.DeleteRiskAnalysAsync(id);

        public async Task<RiskAnalys> GetRiskAnalysAsync(Guid id)
            => await _riskAnalysisRepository.GetRiskAnalysAsync(id);

        public async Task<List<RiskAnalysInfo>> GetRiskAnalysByProcessIdAndTypeAsync(Guid processId, int type)
        {
            var riskForUI = new List<RiskAnalysInfo>();
            var risks = await _riskAnalysisRepository.GetRiskAnalysByProcessIdAndTypeAsync(processId, type);

            if (risks is null || risks.Count <= 0) return riskForUI;

            var users = await _userService.GetAllUsersAsync(false) ?? new List<UserInfo>();

            foreach (var risk in risks)
            {
                var user = users.FirstOrDefault(x => x.Id == risk.UpdatedUserId);

                var newRisk = new RiskAnalysInfo
                {
                    Id = risk.Id,
                    ProcessId = risk.ProcessId,
                    Type = risk.Type,
                    RiskType = risk.RiskType,
                    DefinedIssue = risk.DefinedIssue,
                    PotentialCause = risk.PotentialCause,
                    Degree = risk.Degree,
                    Occurrence = risk.Occurrence,
                    PreventiveAction = risk.PreventiveAction,
                    PersonForSystemImplementation = risk.PersonForSystemImplementation,
                    PlannedSystemImplementationDate = risk.PlannedSystemImplementationDate,
                    RealSystemImplementationDate = risk.RealSystemImplementationDate,
                    ImplementationStatus = risk.ImplementationStatus,
                    SystemPerformance = risk.SystemPerformance,
                    DateEffectivenessOfSystemOperation = risk.DateEffectivenessOfSystemOperation,
                    DegreeAction = risk.DegreeAction,
                    OccurrenceAction = risk.OccurrenceAction,
                    EmergencyPlan = risk.EmergencyPlan,
                    PersonForEmergencyPlan = risk.PersonForEmergencyPlan,
                    AssessmentOfVerificationEffectiveness = risk.AssessmentOfVerificationEffectiveness,
                    OwnerOfProcess = risk.OwnerOfProcess,
                    ChangedFields = risk.ChangedFields == "add" ? string.Empty : !string.IsNullOrWhiteSpace(risk.ChangedFields) ? risk.ChangedFields.Trim(',') : risk.ChangedFields,
                    Rate = risk.Degree * risk.Occurrence,
                    Rate2 = risk.DegreeAction * risk.OccurrenceAction,
                    ModifiedBy = $"{user?.FirstName} {user?.LastName}",
                    ModifiedDate = risk.UpdatedDate,
                    Version = risk.Version
                };

                newRisk.RateClass = GetRateClass(newRisk.Rate ?? 0, newRisk.RiskType);
                newRisk.Rate2Class = GetRateClass(newRisk.Rate2 ?? 0, newRisk.RiskType);

                riskForUI.Add(newRisk);
            }    

            return riskForUI;
        }

        public async Task UpdateRiskAnalysAsync(RiskAnalys risk)
        {
            var currentRisk = await _riskAnalysisRepository.GetRiskAnalysAsync(risk.Id);

            if (currentRisk.CurrentUpdateId != risk.CurrentUpdateId)
            {
                risk.Version = currentRisk.Version + 1;
            }
            else
            {
                risk.Version = currentRisk.Version;
            }

            if (currentRisk.ChangedFields == "add" && currentRisk.CurrentUpdateId == risk.CurrentUpdateId)
            {
                risk.ChangedFields = "add";
                await _riskAnalysisRepository.UpdateRiskAnalysAsync(risk);
            }
            else
            { 
                risk.ChangedFields = SetChangedFields(risk, currentRisk);

                if (currentRisk.CurrentUpdateId == risk.CurrentUpdateId && !string.IsNullOrWhiteSpace(currentRisk.ChangedFields))
                {
                    var fieldsInDb = currentRisk.ChangedFields.Split(',');
                    var fieldsCurrent = risk.ChangedFields.Split(',');

                    var list = fieldsInDb.Union(fieldsCurrent).ToList();
                    list = list.Where(x => x != string.Empty).ToList();
                    risk.ChangedFields = string.Join(",", list);
                }

                await _riskAnalysisRepository.UpdateRiskAnalysAsync(risk);
            }

            
        }

        private string SetChangedFields(RiskAnalys risk, RiskAnalys currentRisk)
        {
            var changedFields = new StringBuilder();

            if (currentRisk.RiskType != risk.RiskType)
            {
                changedFields.Append("R / S,");
            }

            if (currentRisk.DefinedIssue != risk.DefinedIssue)
            {
                changedFields.Append("Zdefiniowane ryzyko,");
            }

            if (currentRisk.PotentialCause != risk.PotentialCause)
            {
                changedFields.Append("Potencjalna przyczyna,");
            }

            if (currentRisk.Degree != risk.Degree)
            {
                changedFields.Append("Stopień ryzyka,");
            }

            if (currentRisk.Occurrence != risk.Occurrence)
            {
                changedFields.Append("Występowalność,");
            }

            if (currentRisk.PreventiveAction != risk.PreventiveAction)
            {
                changedFields.Append("Systemowe działanie zapobiegawcze (R) lub doskonalące (S),");
            }

            if (currentRisk.PersonForSystemImplementation != risk.PersonForSystemImplementation)
            {
                changedFields.Append("Osoba odpowiedzialna za wdrożenie działania systemowego,");
            }

            if (currentRisk.PlannedSystemImplementationDate != risk.PlannedSystemImplementationDate)
            {
                changedFields.Append("Planowana data wdrożenia działania systemowego,");
            }

            if (currentRisk.RealSystemImplementationDate != risk.RealSystemImplementationDate)
            {
                changedFields.Append("Rzeczywista data wdrożenia działania systemowego,");
            }

            if (currentRisk.ImplementationStatus != risk.ImplementationStatus)
            {
                changedFields.Append("Status realizacji działania systemowego,");
            }

            if (currentRisk.SystemPerformance != risk.SystemPerformance)
            {
                changedFields.Append("Ocena skuteczności działania systemowego,");
            }

            if (currentRisk.DateEffectivenessOfSystemOperation != risk.DateEffectivenessOfSystemOperation)
            {
                changedFields.Append("Data oceny skuteczności działania systemowego,");
            }

            if (currentRisk.DegreeAction != risk.DegreeAction)
            {
                changedFields.Append("Stopień,");
            }

            if (currentRisk.OccurrenceAction != risk.OccurrenceAction)
            {
                changedFields.Append("Występowalność,");
            }

            if (currentRisk.EmergencyPlan != risk.EmergencyPlan)
            {
                changedFields.Append("Plan awaryjny,");
            }

            if (currentRisk.PersonForEmergencyPlan != risk.PersonForEmergencyPlan)
            {
                changedFields.Append("Odpowiedzialny za realizację planów awaryjnych,");
            }

            if (currentRisk.AssessmentOfVerificationEffectiveness != risk.AssessmentOfVerificationEffectiveness)
            {
                changedFields.Append("Ocena skuteczności weryfikacji,");
            }

            if (currentRisk.OwnerOfProcess != risk.OwnerOfProcess)
            {
                changedFields.Append("Właściciel processu,");
            }

            return changedFields.ToString();
        }

        private string GetRateClass(int rate, int riskType)
        {
            string classRate = string.Empty;

            if (riskType == 1)
            {
                if (rate > 0 && rate < 3)
                {
                    return "risk-low";
                }
                else if (rate > 2 && rate < 8)
                {
                    return "risk-medium";
                } 
                else if (rate > 7 && rate < 17)
                {
                    return "risk-high";
                }
            }

            if (riskType == 2)
            {
                if (rate > 0 && rate < 3)
                {
                    return "chance-low";
                }
                else if (rate > 2 && rate < 8)
                {
                    return "chance-medium";
                }
                else if (rate > 7 && rate < 17)
                {
                    return "chance-high";
                }
            }

            return classRate;
        }
    }
}
