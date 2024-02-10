using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report.ProcessMap
{
    public class ProcessMapReportModel : IReportModel<ProcessMapModel>
    {

        private readonly ProcessMapModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public ProcessMapReportModel(ProcessMapModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        public IEnumerable<ReportSubReportModel<ProcessMapModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<ProcessMapModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentProcessMap" };
        }
    }
}
