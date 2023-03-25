namespace IdealDiscuss.Entities
{
    public class CommentReportFlag : BaseEntity
    {
        public string CommentReportId { get; set; }
        public CommentReport CommentReport { get; set; }
        public string FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
