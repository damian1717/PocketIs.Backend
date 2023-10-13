namespace PocketIS.Domain.ChartOrg
{
    public class OrgData
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public Guid Id { get; set; }
        public Guid BelowPersonId { get; set; }
    }
}
