using PocketIS.ReportGenerator;

namespace PocketIS.Models.Report
{
    public class ReportModel : IReportModel<QualityPolicyReportModel>
    {
        
        private readonly QualityPolicyReportModel _reportData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reportData">Report data</param>
        public ReportModel(QualityPolicyReportModel reportData)
        {
            _reportData = reportData;
        }

        

        public IEnumerable<ReportSubReportModel<QualityPolicyReportModel>> GetSubReports()
        {
            yield return new ReportSubReportModel<QualityPolicyReportModel> { Model = _reportData, SubReport = "Components/_Content" };
        }
    }
}
