namespace PocketIS.Models.Training
{
    public class UpdateTraining
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ForHowManyMonths { get; set; }
    }
}
