using System.Collections.Generic;

namespace PocketIS.ReportGenerator
{
    /// <summary>
    /// Basic interface for Report Data Models
    /// </summary>
    public interface IReportModel<T>
    {
        IEnumerable<ReportSubReportModel<T>> GetSubReports();
    }
}
