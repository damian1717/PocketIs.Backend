using System.ComponentModel.DataAnnotations;

namespace PocketIS.Domain
{
    public class Company : BaseEntity<Guid>
    {
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string Name { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Dyrektor jest wymagany.")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Nip jest wymagany.")]
        public string Nip { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? Street { get; set; }

        public string? NumberBuilding { get; set; }

        public string? NumberApartment { get; set; }

        public string? KnowHow { get; set; }

        public string? ItemsCompany { get; set; }

        public string? TechnologiesUsed { get; set; }

        public string? CommunicationSystem { get; set; }

        public string? Strengths { get; set; }

        public string? Weaknesses { get; set; }

        public string? OpportunitiesForTheCompany { get; set; }

        public string? ThreatsToTheCompany { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<QualityPolicy> QualityPolicies { get; } = new List<QualityPolicy>();
        public ICollection<Regulation> Regulations { get; } = new List<Regulation>();
        public ICollection<OrganizationChartPerson> OrganizationChartPersons { get; } = new List<OrganizationChartPerson>();
        public ICollection<Document> Documents { get; } = new List<Document>();
    }
}
