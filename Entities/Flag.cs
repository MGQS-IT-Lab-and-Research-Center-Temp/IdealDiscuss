namespace IdealDiscuss.Entities
{
    public class Flag : BaseEntity
    {
        public string FlagName { get; set; }
        public string Description { get; set; }
        public ICollection<CommentReportFlag> CommentReportFlags { get; set; } = new HashSet<CommentReportFlag>();
        public ICollection<QuestionReportFlag> QuestionReportFlags { get; set; } = new HashSet<QuestionReportFlag>();
    }
}
