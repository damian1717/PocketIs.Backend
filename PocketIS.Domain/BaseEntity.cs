namespace PocketIS.Domain
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
        public DateTime InsertedDate { get; set; }
    }
}
