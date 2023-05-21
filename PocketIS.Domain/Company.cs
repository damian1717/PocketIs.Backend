using System.ComponentModel.DataAnnotations;

namespace PocketIS.Domain
{
    public class Company : BaseEntity<Guid>
    {
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dyrektor jest wymagany.")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Nip jest wymagany.")]
        public string Nip { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string NumberBuilding { get; set; }

        public string NumberApartment { get; set; }

        public string KnowHow { get; set; }

        public string ItemsCompany { get; set; }

        public string TechnologiesUsed { get; set; }

        public string CommunicationSystem { get; set; }

        public string Strengths { get; set; }

        public string Weaknesses { get; set; }

        public string OpportunitiesForTheCompany { get; set; }

        public string ThreatsToTheCompany { get; set; }
    }
}
