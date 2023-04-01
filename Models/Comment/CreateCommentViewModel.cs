namespace IdealDiscuss.Models.Comment;

public class CreateCommentViewModel
{
    public string UserId { get; set; }
    public string QuestionId { get; set; }
    public string CommentText { get; set; }
}
