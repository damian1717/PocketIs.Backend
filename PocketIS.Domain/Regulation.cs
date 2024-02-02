namespace PocketIS.Domain
{
    public class Regulation : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Link  { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
