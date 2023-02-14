using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class ViewAllQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
