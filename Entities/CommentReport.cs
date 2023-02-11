namespace IdealDiscuss.Entities
{
    public class CommentReport : BaseEntity
    {
        public int UserId { get; set; }
        public int CommentId { get; set; } 
        public string AdditionalComment { get; set; }
    }
}
