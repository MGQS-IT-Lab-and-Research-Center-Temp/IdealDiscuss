using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.QuestionReport;

public class UpdateQuestionReportViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    [Required(ErrorMessage = " Comment text required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string AdditionalComment { get; set; }
}
