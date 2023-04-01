using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Question;

public class UpdateQuestionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Question text is required")]
    [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
    [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
    public string QuestionText { get; set; }

    public string ImageUrl { get; set; }
}
