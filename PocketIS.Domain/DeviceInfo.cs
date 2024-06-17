namespace PocketIS.Domain
{
    public class DeviceInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int DeviceType { get; set; }
        public DateTime FirstOverviewDate { get; set; }
        public DateTime NextOverviewDate { get; set; }
        public string CssClass { get; set; }
    }
}
