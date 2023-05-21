namespace PocketIS.Domain
{
    public class Regulation : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Link  { get; set; }
        public string Description { get; set; }
    }
}
