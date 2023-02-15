using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
