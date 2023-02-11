namespace IdealDiscuss.Entities
{
    public class CommentReportFlag : BaseEntity
    {
        public int CommentReportId { get; set; }
        public CommentReport CommentReport { get; set; }
        public int FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
