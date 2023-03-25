namespace IdealDiscuss.Entities
{
    public class QuestionReport : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string QuestionId { get; set; } 
        public Question Question { get; set; }
        public string AdditionalComment { get; set; }
        public ICollection<QuestionReportFlag> QuestionReportFlags { get; set; } = new HashSet<QuestionReportFlag>();
    }
}
