namespace IdealDiscuss.Models.QuestionReport;

public class CreateQuestionReportViewModel
{
    public string UserId { get; set; }
    public string QuestionId { get; set; }
    public List<string> FlagIds { get; set; } = new List<string>();
    public string AdditionalComment { get; set; }
}
