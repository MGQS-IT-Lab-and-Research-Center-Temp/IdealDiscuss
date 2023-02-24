namespace IdealDiscuss.Dtos.QuestionReportDto
{
    public class ViewQuestionReportDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string AdditionalComment { get; set; }
    }
}
