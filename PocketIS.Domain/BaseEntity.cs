namespace PocketIS.Domain
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? InsertedUserId { get; set; }
        public Guid? UpdatedUserId { get; set; }
    }
}
