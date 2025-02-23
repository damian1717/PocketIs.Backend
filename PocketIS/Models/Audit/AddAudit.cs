namespace PocketIS.Models.Audit
{
    public class AddAudit
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime AuditDate { get; set; }
        public string Auditor { get; set; }
    }
}
