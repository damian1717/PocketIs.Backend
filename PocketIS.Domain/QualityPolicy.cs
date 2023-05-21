namespace PocketIS.Domain
{
    public class QualityPolicy : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public bool IsExternal { get; set; }
    }
}
