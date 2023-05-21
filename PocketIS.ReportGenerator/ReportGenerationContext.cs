namespace PocketIS.ReportGenerator
{
    public class ReportGenerationContext<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IViewConfig Views { get; set; }

        public LayoutConfig Layout { get; }

        public IReportModel<T> Model { get; }

        public ReportGenerationContext(IReportModel<T> model, IViewConfig views, LayoutConfig layout)
        {
            Views = views;
            Layout = layout;
            Model = model;
            CurrentPage = 1;
            TotalPages = 0;
        }
    }
}
