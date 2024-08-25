namespace PocketIS.Domain
{
    public class RiskAnalysInfo
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public int Type { get; set; }
        public int RiskType { get; set; }
        public string DefinedIssue { get; set; }
        public string PotentialCause { get; set; }
        public int Degree { get; set; }
        public int Occurrence { get; set; }
        public string? PreventiveAction { get; set; }
        public string? PersonForSystemImplementation { get; set; }
        public DateTime? PlannedSystemImplementationDate { get; set; }
        public DateTime? RealSystemImplementationDate { get; set; }
        public string? ImplementationStatus { get; set; }
        public string? SystemPerformance { get; set; }
        public DateTime? DateEffectivenessOfSystemOperation { get; set; }
        public int? DegreeAction { get; set; }
        public int? OccurrenceAction { get; set; }
        public string? EmergencyPlan { get; set; }
        public string? PersonForEmergencyPlan { get; set; }
        public string? AssessmentOfVerificationEffectiveness { get; set; }
        public string? OwnerOfProcess { get; set; }
        public int? Rate { get; set; }
        public int? Rate2 { get; set; }
        public string? RateClass { get; set; }
        public string? Rate2Class { get; set; }
        public string? ChangedFields { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Version { get; set; }
    }
}
