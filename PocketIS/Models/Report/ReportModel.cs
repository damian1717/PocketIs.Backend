using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report
{
    public class ReportModel : IReportModel<QualityPolicyReportModel>
    {
        
        private readonly QualityPolicyReportModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public ReportModel(QualityPolicyReportModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        

        public IEnumerable<ReportSubReportModel<QualityPolicyReportModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<QualityPolicyReportModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentQualityPolicy" };
        }
    }
}
