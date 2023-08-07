using PocketIS.Domain;

namespace PocketIS.ReportGenerator
{
    public class ReportSubReportModel<T>
    {
        public T Model { get; set; }
        public string SubReport { get; set; }
        public bool ShowHeader { get; set; } = true;
        public bool ShowReportInfoInHeader { get; set; } = true;
        public bool ShowFooter { get; set; } = true;
        public string FooterLine1Key { get; set; } = "FooterLine1";
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public User User { get; set; }
    }
}
