namespace PocketIS.Models.Report.DefinitionProcess
{
    public class DefinitionProcessModel : BaseReportModel
    {
        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string Image64String { get; set; }
        public List<Domain.DefinitionOfProcess> DefinitionOfProcesses { get; set; }
    }
}
