using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class ViewCommentReportDto
    {
        public int Id { get; set; }
        public string AdditionalComment { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string CommentReporter { get; set; }
        public string CommentText { get; set; }
        public int FlagId { get; set; }
        public List<string> FlagNames { get; set; } = new List<string>();
    }
}
