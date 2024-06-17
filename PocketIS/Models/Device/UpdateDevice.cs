namespace PocketIS.Models.Device
{
    public class UpdateDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int DeviceType { get; set; }
        public DateTime FirstOverviewDate { get; set; }
        public DateTime NextOverviewDate { get; set; }
    }
}
