using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Question;

public class CreateQuestionViewModel
{

    [Required(ErrorMessage = "One or more Categories need to be selected")]
    public List<string> CategoryIds { get; set; }

    [Required(ErrorMessage = "Question text required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string QuestionText { get; set; }

    public string ImageUrl { get; set; }
}
