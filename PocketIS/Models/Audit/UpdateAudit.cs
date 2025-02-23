namespace PocketIS.Models.Audit
{
    public class UpdateAudit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime AuditDate { get; set; }
        public string Auditor { get; set; }
    }
}
