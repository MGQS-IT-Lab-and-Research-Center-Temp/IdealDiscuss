namespace IdealDiscuss.Models.CommentReport;

public class CommentReportViewModel
{
    public string Id { get; set; }
    public string AdditionalComment { get; set; }
    public string CommentId { get; set; }
    public string UserId { get; set; }
    public string CommentReporter { get; set; }
    public string CommentText { get; set; }
    public string FlagId { get; set; }
    public List<string> FlagNames { get; set; } = new List<string>();
}
