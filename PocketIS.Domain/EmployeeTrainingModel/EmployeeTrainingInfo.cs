namespace PocketIS.Domain.EmployeeTrainingModel
{
    public class EmployeeTrainingInfo
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid TrainingId { get; set; }
        public bool Required { get; set; }
        public DateTime? TrainingDate { get; set; }
        public int? SkillLevel { get; set; }
        public bool Finished { get; set; }
    }
}
