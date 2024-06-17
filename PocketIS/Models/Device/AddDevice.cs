namespace PocketIS.Models.Device
{
    public class AddDevice
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int DeviceType { get; set; }
        public DateTime FirstOverviewDate { get; set; }
        public DateTime NextOverviewDate { get; set; }
    }
}
