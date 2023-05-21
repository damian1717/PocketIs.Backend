using PocketIS.ReportGenerator;

namespace PocketIS.Framwork
{
    internal class ReportViews : IViewConfig
    {
        private static readonly Lazy<ReportViews> Views = new Lazy<ReportViews>(() => new ReportViews
        {
            Header = "ReportHeader",
            Body = "ReportBody",
            Footer = "ReportFooter"
        });

        public string Header { get; private set; }

        public string Body { get; private set; }

        public string Footer { get; private set; }

        public static ReportViews GetDefault()
        {
            return Views.Value;
        }
    }
}
