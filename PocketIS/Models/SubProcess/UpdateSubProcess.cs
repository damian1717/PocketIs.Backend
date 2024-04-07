namespace PocketIS.Models.SubProcess
{
    public class UpdateSubProcess
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public int SubProcessType { get; set; }
        public bool IsArchive { get; set; }
    }
}
