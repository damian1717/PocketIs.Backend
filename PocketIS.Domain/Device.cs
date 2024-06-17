namespace PocketIS.Domain
{
    public class Device : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int DeviceType { get; set; }
        public DateTime FirstOverviewDate { get; set; }
        public DateTime NextOverviewDate { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
