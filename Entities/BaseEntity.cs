using MassTransit;

namespace IdealDiscuss.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = NewId.Next().ToSequentialGuid().ToString();
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; } 
        public DateTime LastModified { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
