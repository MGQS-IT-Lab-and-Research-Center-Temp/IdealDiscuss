namespace IdealDiscuss.Dtos.CommentDto
{
    public class CreateCommentDto
    {
        public string UserId { get; set; }
        public string QuestionId { get; set; }
        public string CommentText { get; set; }
    }
}
