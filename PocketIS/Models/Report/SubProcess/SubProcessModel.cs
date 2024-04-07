namespace PocketIS.Models.Report.SubProcess
{
    public class SubProcessModel : BaseReportModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public int SubProcessType { get; set; }
    }
}
