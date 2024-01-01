namespace PocketIS.Domain
{
    public class Employee : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int Level { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
