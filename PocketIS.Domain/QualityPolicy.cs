namespace PocketIS.Domain
{
    public class QualityPolicy : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public bool IsExternal { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public ICollection<SavedQualityPolicy> SavedQualityPolicies { get; } = new List<SavedQualityPolicy>();
    }
}
