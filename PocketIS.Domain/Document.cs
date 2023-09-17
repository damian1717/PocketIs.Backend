namespace PocketIS.Domain
{
    public class Document : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public byte[] FileData { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
