using MassTransit;

namespace IdealDiscuss.Entities
{
    public abstract class BaseEntity : ISoftDeletable, IAuditBase
    {
        public string Id { get; set; } = NewId.Next().ToSequentialGuid().ToString();
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
