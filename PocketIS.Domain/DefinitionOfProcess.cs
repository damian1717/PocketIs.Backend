namespace PocketIS.Domain
{
    public class DefinitionOfProcess : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsBase { get; set; }
        public Guid ProcessId { get; set; }
    }
}
