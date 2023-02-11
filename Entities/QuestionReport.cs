namespace IdealDiscuss.Entities
{
    public class QuestionReport : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; } 
        public Question Question { get; set; }
        public string AdditionalComment { get; set; }
        public ICollection<QuestionReportFlag> QuestionReportFlags { get; set; } = new HashSet<QuestionReportFlag>();
    }
}
