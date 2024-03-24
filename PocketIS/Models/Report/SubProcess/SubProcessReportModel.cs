using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report.SubProcess
{
    public class SubProcessReportModel : IReportModel<SubProcessModel>
    {

        private readonly SubProcessModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public SubProcessReportModel(SubProcessModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        public IEnumerable<ReportSubReportModel<SubProcessModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<SubProcessModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentSubProcess" };
        }
    }
}
