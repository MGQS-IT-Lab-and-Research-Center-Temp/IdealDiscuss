namespace IdealDiscuss.Entities
{
    public class QuestionReportFlag : BaseEntity
    {
        public string QuestionReportId { get; set; }
        public QuestionReport QuestionReport { get; set; }
        public string FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
