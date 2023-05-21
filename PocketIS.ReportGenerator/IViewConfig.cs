namespace PocketIS.ReportGenerator
{
    public interface IViewConfig
    {
        string Header { get; }

        string Body { get; }

        string Footer { get; }
    }
}
