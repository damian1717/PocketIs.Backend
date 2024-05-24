namespace PocketIS.Domain
{
    public class CompanyInfo
    {
        public CompanyInfo(Guid id, string name, string nip, string city, string postalCode, string street, string numberBuilding, string numberApartment,
            string knowHow, string itemsCompany, string technologiesUsed)
        {
            Id = id;
            Name = name;
            Nip = nip;
            City = city;
            PostalCode = postalCode;
            Street = street;
            NumberBuilding = numberBuilding;
            NumberApartment = numberApartment;
            KnowHow = knowHow;
            ItemsCompany = itemsCompany;
            TechnologiesUsed = technologiesUsed;

            var newPostalCode = !string.IsNullOrWhiteSpace(PostalCode) && PostalCode.Length > 2 ? PostalCode.Insert(2, "-") : PostalCode;
            var number = !string.IsNullOrWhiteSpace(NumberApartment) ? $"{NumberBuilding} / {NumberApartment}" : NumberBuilding;
            Address = $"{newPostalCode} {City}, ul. {Street} {number}";

        }
        public Guid Id { get; }
        public string Name { get; }
        public string Nip { get; }
        public string City { get; }

        public string PostalCode { get; }

        public string Street { get; }

        public string NumberBuilding { get; }

        public string NumberApartment { get; }
        public string KnowHow { get; set; }

        public string ItemsCompany { get; set; }

        public string TechnologiesUsed { get; set; }
        public string Address { get;  }

    }
}
