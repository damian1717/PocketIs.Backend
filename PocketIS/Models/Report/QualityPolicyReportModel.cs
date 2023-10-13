using PocketIS.Models.QualityPolicy;

namespace PocketIS.Models.Report
{
    public class QualityPolicyReportModel : BaseReportModel
    {
        public QualityPolicyModel[] QualityPolicies { get; set; }
    }
}
