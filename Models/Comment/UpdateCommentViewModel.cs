using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Comment;

public class UpdateCommentViewModel
{
    public string Id { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(3, ErrorMessage = "The minimum lenghth is 3.")]
    [MaxLength(150, ErrorMessage = "The Maximum lenghth is 150.")]
    public string CommentText { get; set; }
}
