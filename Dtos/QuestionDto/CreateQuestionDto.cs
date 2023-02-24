using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class CreateQuestionDto
    {
        public int UserId { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
    }
}
