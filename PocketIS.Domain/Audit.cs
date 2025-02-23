namespace PocketIS.Domain
{
    public class Audit : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime AuditDate { get; set; }
        public string Auditor { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
