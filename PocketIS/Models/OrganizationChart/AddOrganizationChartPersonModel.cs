namespace PocketIS.Models.OrganizationChart
{
    public class AddOrganizationChartPersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public Guid? BelowPersonId { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
    }
}
