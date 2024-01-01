namespace PocketIS.Models.EmployeeForTraining
{
    public class UpdateEmployee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int Level { get; set; }
    }
}
