namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class ViewQuestionReportDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionReporter { get; set; }
        public string AdditionalComment { get; set; }
        public string QuestionText { get; set; }
        public List<string> FlagNames { get; set; } = new List<string>();
    }
}
