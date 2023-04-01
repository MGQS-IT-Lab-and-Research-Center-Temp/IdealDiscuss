using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.QuestionReport;

public class CreateQuestionReportViewModel
{
    public string UserId { get; set; }
    public string QuestionId { get; set; }

    [Required(ErrorMessage = "One or more flags need to be selected")]
    public List<string> FlagIds { get; set; } = new List<string>();
    [Required(ErrorMessage = " Comment text required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string AdditionalComment { get; set; }
}
