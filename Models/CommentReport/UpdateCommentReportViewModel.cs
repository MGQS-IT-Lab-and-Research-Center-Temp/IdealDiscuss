using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.CommentReport;

public class UpdateCommentReportViewModel
{
    public string Id { get; set; }
    [Required(ErrorMessage = "Comment text cannot be empty")]
    [MinLength(3, ErrorMessage = "The minimum lenghth is 3.")]
    [MaxLength(200, ErrorMessage = "The Maximum lenghth is 200.")]
    public string AdditionalComment { get; set; }
}
