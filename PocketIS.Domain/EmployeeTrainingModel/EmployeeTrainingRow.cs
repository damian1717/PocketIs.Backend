namespace PocketIS.Domain.EmployeeTrainingModel
{
    public class EmployeeTrainingRow
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string TrainingDate { get; set; }
        public string ColumnName { get; set; }
        public string EmployeeType { get; set; }
        public string ClassOfDisplayTrainingDate { get; set; }
        public string ClassOfDisplayTrainingLevel { get; set; }
    }
}
