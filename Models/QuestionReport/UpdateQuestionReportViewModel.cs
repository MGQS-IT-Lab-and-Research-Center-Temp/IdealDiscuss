namespace IdealDiscuss.Models.QuestionReport;

public class UpdateQuestionReportViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public string AdditionalComment { get; set; }
}
