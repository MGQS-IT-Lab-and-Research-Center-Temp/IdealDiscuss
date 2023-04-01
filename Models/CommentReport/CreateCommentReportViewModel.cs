using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.CommentReport;

public class CreateCommentReportViewModel
{
    public string CommentId { get; set; }
    public string UserId { get; set; }
    public List<string> FlagIds { get; set; } = new List<string>();
    public string QuestionId { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(20, ErrorMessage = "The minimum length is 20.")]
    [MaxLength(200, ErrorMessage = "The Maximum length is 200.")]
    public string AdditionalComment { get; set; }
}
