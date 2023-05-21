namespace PocketIS.Models.QualityPolicy
{
    public class UpdateQualityPolicyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public bool IsExternal { get; set; }
    }
}
