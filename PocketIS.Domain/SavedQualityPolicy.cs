namespace PocketIS.Domain
{
    public class SavedQualityPolicy : BaseEntity<Guid>
    {
        public int Version { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public Guid QualityPolicyId { get; set; }
        public virtual QualityPolicy QualityPolicy { get; set; } = null!;
    }
}
