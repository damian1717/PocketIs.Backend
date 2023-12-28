namespace PocketIS.Domain
{
    public class Training : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int ForHowManyMonths { get; set; }
        public int Level { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
