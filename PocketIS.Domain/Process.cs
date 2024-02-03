namespace PocketIS.Domain
{
    public class Process : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsBaseProcess { get; set; }
        public Guid CompanyId { get; set; }
        public int ProcessType { get; set; }
        public Company Company { get; set; } = null!;
    }
}
