namespace PocketIS.Domain.ChartOrg
{
    public class ChartNode
    {
        public OrgData Data { get; set; }
        public ChartNode[] Children { get; set; }
    }
}
