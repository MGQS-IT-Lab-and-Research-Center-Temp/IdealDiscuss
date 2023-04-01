using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.CommentReport;

public class CreateCommentReportViewModel
{
    public string CommentId { get; set; }
    public string UserId { get; set; }
    public List<string> FlagIds { get; set; } = new List<string>();
    public string QuestionId { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(3, ErrorMessage = "The minimum lenghth is 3.")]
    [MaxLength(150, ErrorMessage = "The Maximum lenghth is 150.")]
    public string AdditionalComment { get; set; }
}
