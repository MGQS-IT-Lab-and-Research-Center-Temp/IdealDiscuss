using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentReport
{
    public class ViewAllCommentReport
    {
        public string AdditionalComment { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
