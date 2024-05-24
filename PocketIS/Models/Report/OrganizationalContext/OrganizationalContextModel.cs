namespace PocketIS.Models.Report.OrganizationalContext
{
    public class OrganizationalContextModel : BaseReportModel
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Nip { get; set; }
        public string? Director { get; set; }

        public string? Industry { get; set; }

        public string? GoalAndStrategy { get; set; }

        public string? TechnologiesOwned { get; set; }

        public string? CommunicationSystem { get; set; }

        public string? FactorsTechnologicalEnvironment { get; set; }

        public string? FactorsRelatedToValuesCulture { get; set; }

        public string? ExternalAndInternalEntities { get; set; }

        public string? Strengths { get; set; }

        public string? Weaknesses { get; set; }

        public string? Chances { get; set; }

        public string? Threats { get; set; }

        public int Norm { get; set; }

        public string? NumberNorm { get; set; }
        public string? NormJustification { get; set; }
    }
}
