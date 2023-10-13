using PocketIS.Domain.ChartOrg;

namespace PocketIS.Models.Report.ChartOrg
{
    public class ChartOrgModel : BaseReportModel
    {

        public string Image64String { get; set; }
        public ChartNode[] ChartNodes { get; set; }
    }
}
