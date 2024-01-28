namespace PocketIS.Domain.EmployeeTrainingModel
{
    public class EmployeeTraining : BaseEntity<Guid>
    {
        public Guid TrainingId { get; set; }
        public bool Required { get; set; }
        public DateTime TrainingDate { get; set; }
        public int SkillLevel { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeType { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
