namespace IdealDiscuss.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public string CommentText { get; set; }
        public ICollection<CommentReport> CommentReports { get; set; } = new HashSet<CommentReport>();
    }
}
