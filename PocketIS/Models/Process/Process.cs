namespace PocketIS.Models.Process
{
    public class Process
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsBaseProcess { get; set; }
        public string CompanyName { get; set; }
        public int ProcessType { get; set; }
    }
}
