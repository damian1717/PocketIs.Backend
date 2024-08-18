namespace PocketIS.Models.RiskAnalys
{
    public class AddRiskAnalys
    {
        public Guid ProcessId { get; set; }
        public int ProcessType { get; set; }
        public string DefinedIssue { get; set; }
        public string PotentialCause { get; set; }
        public int Degree { get; set; }
        public int Occurrence { get; set; }
        public string? OwnerOfProcess { get; set; }
    }
}
