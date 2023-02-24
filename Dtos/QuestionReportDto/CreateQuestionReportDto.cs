using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class CreateQuestionReportDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string AdditionalComment { get; set; }
    }
}
