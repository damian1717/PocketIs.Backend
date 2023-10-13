using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report.ChartOrg
{
    public class ChartOrgReportModel : IReportModel<ChartOrgModel>
    {

        private readonly ChartOrgModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public ChartOrgReportModel(ChartOrgModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        public IEnumerable<ReportSubReportModel<ChartOrgModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<ChartOrgModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentChartOrg" };
        }
    }
}
