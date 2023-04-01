using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Models.Question;

public class UpdateQuestionViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Question text is required")]
    public string QuestionText { get; set; }

    public string ImageUrl { get; set; }
}
