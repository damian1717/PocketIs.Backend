namespace PocketIS.Domain
{
    public class SubProcess : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string HtmlContent { get; set; }
        public int SubProcessType { get; set; }
        public int Version { get; set; }
        public bool IsArchive { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
