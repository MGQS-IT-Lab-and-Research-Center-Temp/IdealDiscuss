using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class CreateQuestionReportDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public List<int> FlagIds { get; set; } = new List<int>();
        public string AdditionalComment { get; set; }
    }
}
