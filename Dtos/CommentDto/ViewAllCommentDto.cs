using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.CommentDto
{
    public class ViewAllCommentDto
    {
        public string CommentText { get; set; }
        public User User { get; set; }
        public Question Question { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
