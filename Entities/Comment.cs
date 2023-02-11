namespace IdealDiscuss.Entities
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string CommentText { get; set; }
    }
}
