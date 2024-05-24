using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report.OrganizationalContext
{
    public class OrganizationalContextReportModel : IReportModel<OrganizationalContextModel>
    {

        private readonly OrganizationalContextModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public OrganizationalContextReportModel(OrganizationalContextModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        public IEnumerable<ReportSubReportModel<OrganizationalContextModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<OrganizationalContextModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentOrganizationalContext" };
        }
    }
}
