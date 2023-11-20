namespace PocketIS.Models.DefinitionOfProcess
{
    public class AddDefinitionOfProcess
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid ProcessId { get; set; }
        public bool IsBase { get; set; }
    }
}
