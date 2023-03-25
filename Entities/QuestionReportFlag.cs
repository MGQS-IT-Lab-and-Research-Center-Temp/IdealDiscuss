namespace IdealDiscuss.Entities
{
    public class QuestionReportFlag
    {
        public int QuestionReportId { get; set; }
        public QuestionReport QuestionReport { get; set; }
        public int FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
