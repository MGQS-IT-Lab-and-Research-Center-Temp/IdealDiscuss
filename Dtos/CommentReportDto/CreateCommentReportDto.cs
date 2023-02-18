using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class CreateCommentReportDto
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int FlagId { get; set; }
        public string AdditionalComment { get; set; }
    }
}
