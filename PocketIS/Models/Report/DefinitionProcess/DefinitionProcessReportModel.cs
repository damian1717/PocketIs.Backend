using PocketIS.Domain;
using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report.DefinitionProcess
{
    public class DefinitionProcessReportModel : IReportModel<DefinitionProcessModel>
    {

        private readonly DefinitionProcessModel _reportData;
        private readonly UserInfo _user;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        /// <param name="user">User Info</param>
        public DefinitionProcessReportModel(DefinitionProcessModel reportData, UserInfo user)
        {
            _reportData = reportData;
            _user = user;
        }

        public IEnumerable<ReportSubReportModel<DefinitionProcessModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<DefinitionProcessModel> { Model = _reportData, User = _user, SubReport = "Components/Content/_ContentDefinitionProcess" };
        }
    }
}
