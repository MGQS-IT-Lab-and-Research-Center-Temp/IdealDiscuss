using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class Editquestionreport
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AdditionalComment { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
    }
}
