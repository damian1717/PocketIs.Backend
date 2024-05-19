namespace PocketIS.Models.Company
{
    public class UpdateCompanyModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
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
        public string ContactDetails { get; set; }
        public bool IsArchive { get; set; }
    }
}
