namespace IdealDiscuss.Entities
{
    public class CommentReport : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CommentId { get; set; } 
        public Comment Comment { get; set; }
        public string AdditionalComment { get; set; }
        public ICollection<CommentReportFlag> CommentReportFlags { get; set; } = new HashSet<CommentReportFlag>();
    }
}
