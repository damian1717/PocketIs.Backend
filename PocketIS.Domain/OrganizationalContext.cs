﻿namespace PocketIS.Domain
{
    public class OrganizationalContext : BaseEntity<Guid>
    {
        public string? Industry { get; set; }

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
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
