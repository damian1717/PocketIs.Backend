namespace PocketIS.Domain.BusinessEntity
{
    public class OrganizationChartPersonModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public Guid? BelowPersonId { get; set; }
        public string BelowPersonName { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
    }
}
