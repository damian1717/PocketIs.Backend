namespace PocketIS.Models.EmployeeTraining
{
    public class UpdateEmployeeTraining
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public bool Required { get; set; }
        public DateTime TrainingDate { get; set; }
        public int SkillLevel { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
