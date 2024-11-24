using PocketIS.Domain.EmployeeTrainingModel;
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
        public string? ContactDetails { get; set; }
        public bool IsArchive { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<QualityPolicy> QualityPolicies { get; } = new List<QualityPolicy>();
        public ICollection<Regulation> Regulations { get; } = new List<Regulation>();
        public ICollection<OrganizationChartPerson> OrganizationChartPersons { get; } = new List<OrganizationChartPerson>();
        public ICollection<Document> Documents { get; } = new List<Document>();
        public ICollection<SavedQualityPolicy> SavedQualityPolicies { get; } = new List<SavedQualityPolicy>();
        public ICollection<Process> Processes { get; } = new List<Process>();
        public ICollection<Training> Trainings { get; } = new List<Training>();
        public ICollection<Employee> EmployeeForTrainings { get; } = new List<Employee>();
        public ICollection<EmployeeTraining> EmployeeTrainings { get; } = new List<EmployeeTraining>();
        public ICollection<OrganizationalContext> OrganizationalContexts { get; } = new List<OrganizationalContext>();
        public ICollection<Device> Devices { get; } = new List<Device>();
        public ICollection<Complaint> Complaints { get; } = new List<Complaint>();
    }
}
