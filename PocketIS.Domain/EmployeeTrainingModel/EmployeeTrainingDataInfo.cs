namespace PocketIS.Domain.EmployeeTrainingModel
{
    public class EmployeeTrainingDataInfo
    {
        public int Position { get; set; }
        public string TrainingName { get; set; }
        public List<EmployeeTrainingRow> EmployeeTrainings { get; set; }
    }
}
