using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class ViewAllCommentReportDto
    {
        public string AdditionalComment { get; set; }
        public int CommentId { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
