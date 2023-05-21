using PdfSharp.Pdf;

namespace PocketIS.ReportGenerator
{
    public class PdfDocumentModelPair<T>
    {
        public PdfDocument Document { get; set; }
        public ReportSubReportModel<T> Model { get; set; }
    }
}
