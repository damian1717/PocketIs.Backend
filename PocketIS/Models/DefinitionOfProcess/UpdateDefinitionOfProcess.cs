namespace PocketIS.Models.DefinitionOfProcess
{
    public class UpdateDefinitionOfProcess
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid ProcessId { get; set; }
        public bool IsBase { get; set; }
    }
}
