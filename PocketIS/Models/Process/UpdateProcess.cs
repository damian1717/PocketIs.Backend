namespace PocketIS.Models.Process
{
    public class UpdateProcess
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsBaseProcess { get; set; }
        public int ProcessType { get; set; }
    }
}
