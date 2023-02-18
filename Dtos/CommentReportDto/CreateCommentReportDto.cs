using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class CreateCommentReportDto
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public List<int> FlagIds { get; set; } = new List<int>();
        public string AdditionalComment { get; set; }
    }
}
