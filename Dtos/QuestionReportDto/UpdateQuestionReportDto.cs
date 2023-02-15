using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class UpdateQuestionReportDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AdditionalComment { get; set; }
    }
}
