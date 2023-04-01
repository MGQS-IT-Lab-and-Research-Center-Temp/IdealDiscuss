using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Comment;

public class CreateCommentViewModel
{
    public string UserId { get; set; }
    public string QuestionId { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(3, ErrorMessage = "The minimum length is 3.")]
    public string CommentText { get; set; }
}
